using Academia.Translogix.WebApi._Features.Gral.Dtos;
using Academia.Translogix.WebApi._Features.Gral.Services;
using Academia.Translogix.WebApi._Features.Viaj.Dtos;
using Academia.Translogix.WebApi.Common._ApiResponses;
using Academia.Translogix.WebApi.Common._BaseDomain;
using Academia.Translogix.WebApi.Infrastructure;
using Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase;
using Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase.Entities.Acce;
using Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase.Entities.Gral;
using Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase.Entities.Viaj;
using AutoMapper;
using Farsiman.Domain.Core.Standard.Repositories;
using Microsoft.EntityFrameworkCore;
using static Academia.Translogix.WebApi._Features.Gral.Services._OpenRouteService;
using static Academia.Translogix.WebApi._Features.Viaj.Dtos.ViajesDetallesDto;
using static Academia.Translogix.WebApi._Features.Viaj.Dtos.ViajesDto;

namespace Academia.Translogix.WebApi._Features.Viaj.Services
{
    public class ViajeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly _OpenRouteService _openRouteService;
        private readonly IMapper _mapper;
        private readonly ViajeDominioService _viajeDominioService;

        public ViajeService(_OpenRouteService openRouteService, UnitOfWorkBuilder unitOfWork, IMapper mapper, ViajeDominioService viajeDominioService)
        {
            _unitOfWork = unitOfWork.BuildDbTranslogix();
            _openRouteService = openRouteService;
            _mapper = mapper;
            _viajeDominioService = viajeDominioService;
        }

        public ApiResponse<List<ViajesDto>> ObtenerTodos()
        {
            try
            {
                var lista = _unitOfWork.Repository<Viajes>().AsQueryable().AsNoTracking().ToList();

                var listaDto = _mapper.Map<List<ViajesDto>>(lista);

                return ApiResponseHelper.Success(listaDto, "Registros obtenidos con éxito");
            }
            catch (Exception ex)
            {
                var response = ApiResponseHelper.ErrorDto<List<ViajesDto>>("Error al obtener registros: " + ex.Message);
                if (response.Data == null)
                {
                    response.Data = [];
                }
                return response;
            }
        }

        public async Task<ApiResponse<List<RutaAgrupadaResponse>>> AgruparCrearRutas(ViajesModeloInsertarDto request)
        {
            try
            {

                var noNulos = BaseDomainHelpers.ValidarCamposNulosVacios(request);

                if (!noNulos.Success)
                {
                    return ApiResponseHelper.ErrorDto<List<RutaAgrupadaResponse>>($"No se aceptan valores nulo: {noNulos.Message}");
                }

                var usuarioEntidad = (from usu in _unitOfWork.Repository<Usuarios>().AsQueryable().AsNoTracking()
                                      where usu.usuario_id == request.usuario_creacion
                                      select usu).FirstOrDefault();
                bool esNull = _viajeDominioService.esNulo(usuarioEntidad);
                if (!esNull)
                {
                    return ApiResponseHelper.ErrorDto<List<RutaAgrupadaResponse>>("No se encontro ningun registro de usuario para el Id del usuario enviado");
                }

                bool esAdmin = _viajeDominioService.esAdmin(usuarioEntidad);
                if (!esAdmin)
                {
                    return ApiResponseHelper.ErrorDto<List<RutaAgrupadaResponse>>("Solo los usuarios con rol de administrador pueden crear viajes");
                }

                var origin = new double[] { request.Origen.Longitude, request.Origen.Latitude };
                var ubicaciones = request.Ubicaciones.Select(u => new UbicacionesViaje
                {
                    Ubicaciones = new double[] { u.Longitud, u.Latitud },
                    DistanciaKm = u.DistanciaKm,
                    colaborador_id = u.colaborador_id
                }).ToList();



                var result = await _openRouteService.AgruparRutasPorDistanciaAsync(origin, ubicaciones, request.transportista);
                var conteo = result.RutasAgrupadas.Count;

                _unitOfWork.BeginTransaction();

                foreach (var viaje in result.RutasAgrupadas)
                {

                    var modeloInsertarViaje = new ViajesInsertarDto
                    {
                        distancia_recorrida_km = viaje.Colaboradores.LastOrDefault()?.DistanciaKm ?? 0,
                        fecha = request.fecha_hora,
                        fecha_creacion = DateTime.Now,
                        sucursal_id = request.Origen.sucursal_id,
                        total_pagar = viaje.TotalPagoPorViaje,
                        transportista_id = viaje.transportista_id,
                        usuario_id = request.usuario_creacion,
                        usuario_creacion = request.usuario_creacion,
                        es_activo = true
                    };

                    var entidadViaje = _mapper.Map<Viajes>(modeloInsertarViaje);
                    _unitOfWork.Repository<Viajes>().Add(entidadViaje);

                    if (!_unitOfWork.SaveChanges())
                    {
                        _unitOfWork.RollBack();
                        return ApiResponseHelper.ErrorDto<List<RutaAgrupadaResponse>>("Error al realizar la operación");
                    }

                    var modeloViajeDetalles = viaje.Colaboradores.Select(colaborador => new ViajesDetalleInsertarDto
                    {
                        colaborador_id = colaborador.ColaboradorId,
                        es_activo = true,
                        fecha_creacion = DateTime.Now,
                        total_pagar_por_km = colaborador.TotalPagoPorColaborador,
                        usuario_creacion = request.usuario_creacion,
                        viaje_id = entidadViaje.viaje_id
                    }).ToList();


                    var entidadesViajeDetalles = _mapper.Map<List<Viajes_Detalles>>(modeloViajeDetalles);

                    _unitOfWork.Repository<Viajes_Detalles>().AddRange(entidadesViajeDetalles);

                    if (!_unitOfWork.SaveChanges())
                    {
                        _unitOfWork.RollBack();
                        return ApiResponseHelper.ErrorDto<List<RutaAgrupadaResponse>>("Error al realizar la operación");
                    }

                }

                    _unitOfWork.Commit();
                return ApiResponseHelper.Success(result.RutasAgrupadas, result.Mensaje);
            }
            catch (Exception ex)
            {
                return ApiResponseHelper.ErrorDto<List<RutaAgrupadaResponse>>($"Error al realizar la operación: {ex.Message}");
            }
        }

        

    }
}
