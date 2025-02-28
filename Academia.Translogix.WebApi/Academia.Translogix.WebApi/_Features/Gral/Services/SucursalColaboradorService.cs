using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Academia.Translogix.WebApi._Features.Gral.Dtos;
using Academia.Translogix.WebApi._Features.Viaj;
using Academia.Translogix.WebApi._Features.Viaj.Dtos;
using Academia.Translogix.WebApi.Common._ApiResponses;
using Academia.Translogix.WebApi.Common._BaseDomain;
using Academia.Translogix.WebApi.Common._BaseService;
using Academia.Translogix.WebApi.Infrastructure;
using Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase;
using Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase.Entities.Gral;
using Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase.Entities.Viaj;
using AutoMapper;
using Azure.Core.Pipeline;
using Farsiman.Domain.Core.Standard.Repositories;
using Microsoft.EntityFrameworkCore;
using static Academia.Translogix.WebApi._Features.Gral.Services.GoogleMapsService;

namespace Academia.Translogix.WebApi._Features.Gral.Services
{
    public class SucursalColaboradorService : BaseService<Sucursales_Colaboradores, SucursalesColaboradoresDto, SucursalesColaboradoresInsertarDto, SucursalesColaboradoresActualizarDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ColaboradorService _colaboradorService;
        private readonly SucursalService _sucursalService;
        private readonly GoogleMapsService _googleMapsService;
        public readonly GeneralDominioService _dominioService;

        public SucursalColaboradorService(IMapper mapper,
            UnitOfWorkBuilder unitOfWork,
                                          ColaboradorService colaboradorService,
                                          SucursalService sucursalService,
                                          GoogleMapsService googleMapsService,
                                          GeneralDominioService DominioService)
            : base(mapper, unitOfWork)
        {
            _unitOfWork = unitOfWork.BuildDbTranslogix();
            _sucursalService = sucursalService;
            _mapper = mapper;
            _colaboradorService = colaboradorService;
            _googleMapsService = googleMapsService;
            _dominioService = DominioService;
        }

        public async Task<ApiResponse<List<string>>> InsertarAsync(SucursalesColaboradoresInsertarDto modelo)
        {
            try
            {
                foreach (var item in modelo.SucursalesColaboradores)
                {
                    var noNulos = BaseDomainHelpers.ValidarCamposNulosVacios(item);
                    if (!noNulos.Success)
                    {
                        return ApiResponseHelper.ErrorDto<List<string>>("No se aceptan valores nulos: " + noNulos.Message);
                    }
                }

                var resultados = new List<string>(); 

                foreach (var sucursalColaborador in modelo.SucursalesColaboradores)
                {

                    var entidad = _mapper.Map<Sucursales_Colaboradores>(sucursalColaborador);

                    var resulSucursales = (from su in _unitOfWork.Repository<Sucursales>().AsQueryable().AsNoTracking()
                                           where su.sucursal_id == entidad.sucursal_id
                                           select su).FirstOrDefault();

                    var resulColaboradores = (from col in _unitOfWork.Repository<Colaboradores>().AsQueryable().AsNoTracking()
                                              where col.colaborador_id == entidad.colaborador_id
                                              select col).FirstOrDefault();

                    if (resulColaboradores == null || resulSucursales == null)
                    {
                        resultados.Add("Colaborador o sucursal no encontrados");
                        continue;
                    }

                    var personas = (from persona in _unitOfWork.Repository<Personas>().AsQueryable().AsNoTracking().ToList()
                                    select persona);
                    var colaboradores = personas.Where(x => x.persona_id == resulColaboradores.persona_id).FirstOrDefault();

                    var resultSucursalesColaborador = (from sucol in _unitOfWork.Repository<Sucursales_Colaboradores>().AsQueryable().AsNoTracking()
                                                       where sucol.colaborador_id == entidad.colaborador_id && sucol.sucursal_id == entidad.sucursal_id
                                                       select sucol).FirstOrDefault();

                    if (resultSucursalesColaborador != null)
                    {
                        resultados.Add($"No se puede asignar un colaborador a la misma sucursal 2 veces para el colaborador: {colaboradores.primer_nombre} {colaboradores.primer_apellido}" );
                        continue; 
                    }

                    var origen = new Location { Lat = resulColaboradores.latitud, Lng = resulColaboradores.longitud };
                    var destino = new Location { Lat = resulSucursales.latitud, Lng = resulSucursales.longitud };

                    var distancia = await _googleMapsService.CalcularDistanciaAsync(origen, destino);
                    decimal distanciaKm = ConvertirDistanciaAKilometros(distancia);

                    if (_dominioService.ValidarDistancia(distanciaKm))
                    {
                        sucursalColaborador.distancia_empleado_sucursal_km = distanciaKm;
                        var entidadModelo = _mapper.Map<Sucursales_Colaboradores>(sucursalColaborador);
                        _unitOfWork.Repository<Sucursales_Colaboradores>().Add(entidadModelo);
                        var resultadoInsercion = await _unitOfWork.SaveChangesAsync();
                        resultados.Add(resultadoInsercion ? $"Registro insertado correctamente para el colaborador: { colaboradores.primer_nombre} { colaboradores.primer_apellido}" :
                                                            $"No se pudo insertar correctamente parra colaborador: { colaboradores.primer_nombre} { colaboradores.primer_apellido}");
                    }
                    else
                    {
                        resultados.Add($"La distancia entre la casa del colaborador no puede ser 0 ni mayor de 50km para el colaborador:   {colaboradores.primer_nombre} {colaboradores.primer_apellido}");
                    }
                }

                return ApiResponseHelper.Success(resultados);
            }
            catch (Exception ex)
            {
                return ApiResponseHelper.ErrorDto<List<string>>("Error al procesar la lista: " + ex.Message );
            }
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
            try
            {
                var resultado = (from col in _unitOfWork.Repository<Colaboradores>().AsQueryable().AsNoTracking()
                                 join suc in _unitOfWork.Repository<Sucursales_Colaboradores>().AsQueryable().AsNoTracking()
                                     on col.colaborador_id equals suc.colaborador_id
                                 join su in _unitOfWork.Repository<Sucursales>().AsQueryable().AsNoTracking()
                                     on suc.sucursal_id equals su.sucursal_id
                                 where col.es_activo == true
                                       && suc.es_activo == true
                                       && su.es_activo == true
                                       && suc.sucursal_id == sucursal_id
                                 select new ColaboradoresSucursales
                                 {
                                     colaborador_id = col.colaborador_id,
                                     sucursal_id = su.sucursal_id,
                                     distancia_empleado_sucursal_km = suc.distancia_empleado_sucursal_km,
                                     latitudColaborador = col.latitud,
                                     longitudColaborador = col.longitud,
                                     latitudSucursal = su.latitud,
                                     longitudSucursal = su.longitud
                                 }).ToList();

                return ApiResponseHelper.Success(resultado, "Datos Obtenidos");
            }
            catch (Exception ex)
            {
                return ApiResponseHelper.ErrorDto<List<ColaboradoresSucursales>>("Error al obtener los datos: " + ex);
            }
        }

        public ApiResponse<List<ColaboradoresSucursalesNoAsignadosDto>> ObtenerColaboradoresPorSucursalesNoAsignados(int sucursal_id)
        {
            try
            {
                //if(sucursal_id == 0 || sucursal_id == null)
                //{
                //    return ApiResponseHelper.ErrorDto<List<ColaboradoresSucursalesNoAsignadosDto>>("El Id de la sucursal esta nulo o vacio");
                //}

                var resulSucursales = (from su in _unitOfWork.Repository<Sucursales>().AsQueryable().AsNoTracking()
                                       where su.sucursal_id == sucursal_id
                                       select su).FirstOrDefault();
                if(resulSucursales == null)
                {
                    return ApiResponseHelper.ErrorDto<List<ColaboradoresSucursalesNoAsignadosDto>>("No se encontro ninguna sucursal con ese Id");
                }

                var resultado = (from col in _unitOfWork.Repository<Colaboradores>().AsQueryable().AsNoTracking()
                                 join perso in _unitOfWork.Repository<Personas>().AsQueryable().AsNoTracking()
                                     on col.persona_id equals perso.persona_id
                                 where col.es_activo == true && perso.es_activo == true &&
                                      !_unitOfWork.Repository<Sucursales_Colaboradores>().AsQueryable().AsNoTracking()
                                         .Any(sucu => sucu.colaborador_id == col.colaborador_id &&
                                                    sucu.sucursal_id == sucursal_id &&
                                                    sucu.es_activo == true)
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
                return ApiResponseHelper.ErrorDto<List<ColaboradoresSucursalesNoAsignadosDto>>("Error al obtener los datos: " + ex);
            }
        }

    }
}