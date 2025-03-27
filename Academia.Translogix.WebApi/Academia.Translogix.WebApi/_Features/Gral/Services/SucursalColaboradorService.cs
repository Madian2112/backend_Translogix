using System.Globalization;
using Academia.Translogix.WebApi._Features.Gral.Dtos;
using Academia.Translogix.WebApi.Common;
using Academia.Translogix.WebApi.Common._ApiResponses;
using Academia.Translogix.WebApi.Common._BaseDomain;
using Academia.Translogix.WebApi.Infrastructure;
using Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase.Entities.Gral;
using Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase.Entities.Viaj;
using AutoMapper;
using Farsiman.Domain.Core.Standard.Repositories;
using Microsoft.EntityFrameworkCore;
using static Academia.Translogix.WebApi._Features.Gral.Services.GoogleMapsService;

namespace Academia.Translogix.WebApi._Features.Gral.Services
{
    public class SucursalColaboradorService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly GoogleMapsService _googleMapsService;
        public readonly GeneralDominioService _dominioService;

        public SucursalColaboradorService(IMapper mapper, UnitOfWorkBuilder unitOfWork, GoogleMapsService googleMapsService,
                                          GeneralDominioService DominioService)
        {
            _unitOfWork = unitOfWork.BuildDbTranslogix();
            _mapper = mapper;
            _googleMapsService = googleMapsService;
            _dominioService = DominioService;
        }

        public async Task<ApiResponse<List<string>>> InsertarAsync(SucursalesColaboradoresInsertarDto modelo)
        {
            try
            {
                var validationResult = ValidateInputModel(modelo);
                if (!validationResult.Success)
                {
                    return validationResult;
                }

                var resultados = new List<string>();
                var resultadosExitosos = new List<string>();

                foreach (var sucursalColaborador in modelo.SucursalesColaboradores)
                {
                    var entidad = _mapper.Map<SucursalesColaboradores>(sucursalColaborador);

                    var resulSucursales = _unitOfWork.Repository<Sucursales>()
                        .AsQueryable().AsNoTracking()
                        .FirstOrDefault(su => su.sucursal_id == entidad.sucursal_id);

                    var resulColaboradores = _unitOfWork.Repository<Colaboradores>()
                        .AsQueryable().AsNoTracking()
                        .FirstOrDefault(col => col.colaborador_id == entidad.colaborador_id);

                    if (!ValidarExistenciaEntidades(resulSucursales ?? new Sucursales(), resulColaboradores ?? new Colaboradores()))
                    {
                        resultados.Add(Mensajes._17_Colaborador_Sucursal_No_Encontrado);
                        continue;
                    }

                    var personaId = resulColaboradores?.persona_id ?? 0;
                    var colaboradores = _unitOfWork.Repository<Personas>()
                        .AsQueryable().AsNoTracking()
                        .FirstOrDefault(x => x.persona_id == personaId);

                    if (ValidarDuplicado(entidad))
                    {
                        resultados.Add(string.Format(
                            Mensajes._18_Colaborador_Sucursal_Duplicada,
                            colaboradores?.primer_nombre,
                            colaboradores?.primer_apellido
                        ));
                        continue;
                    }

                    var (distanciaValida, distanciaKm) = await CalcularValidarDistancia(resulSucursales ?? new Sucursales(), resulColaboradores ?? new Colaboradores());
                    if (!distanciaValida)
                    {
                        resultados.Add(string.Format(
                            Mensajes._21_Colaborador_Distancia_Invalida,
                            colaboradores?.primer_nombre,
                            colaboradores?.primer_apellido
                        ));
                        continue;
                    }

                    // Insertar registro
                    sucursalColaborador.distancia_empleado_sucursal_km = distanciaKm;
                    var entidadModelo = _mapper.Map<SucursalesColaboradores>(sucursalColaborador);
                    _unitOfWork.Repository<SucursalesColaboradores>().Add(entidadModelo);

                    var resultadoInsercion = await _unitOfWork.SaveChangesAsync();

                    if (resultadoInsercion)
                    {
                        resultadosExitosos.Add(
                            string.Format(Mensajes._19_Colaborador_Insertado,
                            colaboradores?.primer_nombre,
                            colaboradores?.primer_apellido)
                        );
                    }
                    else
                    {
                        resultados.Add(
                            string.Format(Mensajes._20_Error_Colaborador_Insertar,
                            colaboradores?.primer_nombre,
                            colaboradores?.primer_apellido)
                        );
                    }
                }

                return resultados.Count > 0
                    ? ApiResponseHelper.Success(resultados, "Operacion Fallida", 400)
                    : ApiResponseHelper.Success(resultadosExitosos);
            }
            catch (Exception ex)
            {
                return ApiResponseHelper.ErrorDto(new List<string>(), Mensajes._15_Error_Operacion + ex.Message);
            }
        }

        private static ApiResponse<List<string>> ValidateInputModel(SucursalesColaboradoresInsertarDto modelo)
        {
            foreach (var item in modelo.SucursalesColaboradores)
            {
                var noNulos = BaseDomainHelpers.ValidarCamposNulosVacios(item);
                if (!noNulos.Success)
                {
                    return ApiResponseHelper.ErrorDto(new List<string>(),
                        Mensajes._06_Valores_Nulos + noNulos.Message);
                }
            }
            return ApiResponseHelper.Success(new List<string>());
        }

        private static bool ValidarExistenciaEntidades(Sucursales sucursales, Colaboradores colaboradores)
        {
            return BaseDomainHelpers.ValidadObtenerDatosNulos(sucursales).Success &&
                   BaseDomainHelpers.ValidadObtenerDatosNulos(colaboradores).Success;
        }

        private bool ValidarDuplicado(SucursalesColaboradores entidad)
        {
            return _unitOfWork.Repository<SucursalesColaboradores>()
                .AsQueryable()
                .AsNoTracking()
                .Any(sucol =>
                    sucol.colaborador_id == entidad.colaborador_id &&
                    sucol.sucursal_id == entidad.sucursal_id);
        }

        private async Task<(bool Valida, decimal Distancia)> CalcularValidarDistancia(
            Sucursales sucursales, Colaboradores colaboradores)
        {
            var origen = new Location
            {
                Lat = colaboradores?.latitud ?? 0,
                Lng = colaboradores?.longitud ?? 0
            };
            var destino = new Location
            {
                Lat = sucursales?.latitud ?? 0,
                Lng = sucursales?.longitud ?? 0
            };

            var distancia = await _googleMapsService.CalcularDistanciaAsync(origen, destino);
            decimal distanciaKm = ConvertirDistanciaAKilometros(distancia);

            return (_dominioService.ValidarDistancia(distanciaKm), distanciaKm);
        }

        public decimal ConvertirDistanciaAKilometros(string distanciaTexto)
        {
            distanciaTexto = distanciaTexto.Trim();

            string[] partes = distanciaTexto.Split(' ');
            if (partes.Length != 2)
            {
                throw new FormatException($"Formato de distancia no válido: {distanciaTexto}");
            }

            if (!decimal.TryParse(partes[0], NumberStyles.Any, CultureInfo.InvariantCulture, out decimal valor))
            {
                throw new FormatException($"No se pudo convertir el valor a número: {partes[0]}");
            }

            string unidad = partes[1].ToLower();
            switch (unidad)
            {
                case "km":
                    return valor;
                case "m":
                    return valor / 1000m;
                default:
                    throw new FormatException($"Unidad de distancia no reconocida: {unidad}");
            }
        }

        public ApiResponse<List<ColaboradoresSucursales>> ObtenerColaboradoresPorSucursales(int sucursal_id)
        {
            List<ColaboradoresSucursales> data = new List<ColaboradoresSucursales>();
            try
            {
                var resultado = (from col in _unitOfWork.Repository<Colaboradores>().AsQueryable().AsNoTracking()
                                 join perso in _unitOfWork.Repository<Personas>().AsQueryable().AsNoTracking()
                                    on col.persona_id equals perso.persona_id
                                 join suc in _unitOfWork.Repository<SucursalesColaboradores>().AsQueryable().AsNoTracking()
                                     on col.colaborador_id equals suc.colaborador_id
                                 join su in _unitOfWork.Repository<Sucursales>().AsQueryable().AsNoTracking()
                                     on suc.sucursal_id equals su.sucursal_id
                                 where col.es_activo
                                       && suc.es_activo
                                       && su.es_activo
                                       && suc.sucursal_id == sucursal_id
                                 select new ColaboradoresSucursales
                                 {
                                     colaborador_id = col.colaborador_id,
                                     sucursal_id = su.sucursal_id,
                                     distancia_empleado_sucursal_km = suc.distancia_empleado_sucursal_km,
                                     latitudColaborador = col.latitud,
                                     longitudColaborador = col.longitud,
                                     latitudSucursal = su.latitud,
                                     longitudSucursal = su.longitud,
                                     primer_nombre = perso.primer_nombre,
                                     primer_apellido = perso.primer_apellido
                                 }).ToList();

                return ApiResponseHelper.Success(resultado, "Datos Obtenidos");
            }
            catch (Exception ex)
            {
                return ApiResponseHelper.ErrorDto(data, "Error al obtener los datos: " + ex);
            }
        }

        public ApiResponse<List<ColaboradoresSucursalesNoAsignadosDto>> ObtenerColaboradoresPorSucursalesNoAsignados(int sucursal_id)
        {
            List<ColaboradoresSucursalesNoAsignadosDto> data = new List<ColaboradoresSucursalesNoAsignadosDto>();
            try
            {
                var resulSucursales = (from su in _unitOfWork.Repository<Sucursales>().AsQueryable().AsNoTracking()
                                       where su.sucursal_id == sucursal_id
                                       select su).FirstOrDefault();
                if (resulSucursales == null)
                {
                    return ApiResponseHelper.ErrorDto(data, "No se encontro ninguna sucursal con ese Id");
                }

                var resultado = (from col in _unitOfWork.Repository<Colaboradores>().AsQueryable().AsNoTracking()
                                 join perso in _unitOfWork.Repository<Personas>().AsQueryable().AsNoTracking()
                                     on col.persona_id equals perso.persona_id
                                 where col.es_activo && perso.es_activo &&
                                      !_unitOfWork.Repository<SucursalesColaboradores>().AsQueryable().AsNoTracking()
                                         .Any(sucu => sucu.colaborador_id == col.colaborador_id &&
                                                    sucu.sucursal_id == sucursal_id &&
                                                    sucu.es_activo)
                                 select new ColaboradoresSucursalesNoAsignadosDto
                                 {
                                     colaborador_id = col.colaborador_id,
                                     identidad = perso.identidad,
                                     primer_nombre = perso.primer_nombre,
                                     primer_apellido = perso.primer_apellido
                                 }).ToList();

                return ApiResponseHelper.Success(resultado, "Datos Obtenidos");
            }
            catch (Exception ex)
            {
                return ApiResponseHelper.ErrorDto(data, "Error al obtener los datos: " + ex);
            }
        }

        public ApiResponse<List<SucursalesColaboradoresDto>> ObtenerTodos()
        {
            List<SucursalesColaboradoresDto> data = new List<SucursalesColaboradoresDto>();
            try
            {
                var lista = _unitOfWork.Repository<SucursalesColaboradores>().AsQueryable().AsNoTracking().ToList();
                var listaDto = _mapper.Map<List<SucursalesColaboradoresDto>>(lista);
                return ApiResponseHelper.Success(listaDto, Mensajes._02_Registros_Obtenidos);
            }
            catch (Exception ex)
            {
                int statusCode = 400;
                string errorMessage = Mensajes._03_Error_Registros_Obtenidos + ex.Message;

                if (ex is Microsoft.Data.SqlClient.SqlException ||
                    ex.Message.Contains("timeout", StringComparison.OrdinalIgnoreCase) ||
                    ex.Message.Contains("database", StringComparison.OrdinalIgnoreCase))
                {
                    statusCode = 500;
                    errorMessage = "Error de base de datos: " + ex.Message;
                }

                var response = new ApiResponse<List<SucursalesColaboradoresDto>>(false, errorMessage, data, statusCode);
                if (response.Data == null)
                {
                    response.Data = [];
                }
                return response;
            }
        }


    }
}