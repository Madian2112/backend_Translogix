using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Academia.Translogix.WebApi._Features.Gral.Dtos;
using Academia.Translogix.WebApi._Features.Viaj;
using Academia.Translogix.WebApi._Features.Viaj.Dtos;
using Academia.Translogix.WebApi.Infrastructure._ApiResponses;
using Academia.Translogix.WebApi.Infrastructure._BaseService;
using Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase;
using Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase.Entities.Viaj;
using AutoMapper;
using static Academia.Translogix.WebApi._Features.Gral.Services.GoogleMapsService;

namespace Academia.Translogix.WebApi._Features.Gral.Services
{
    public class SucursalColaboradorService : BaseService<Sucursales_Colaboradores, SucursalesColaboradoresDto, SucursalesColaboradoresInsertarDto, SucursalesColaboradoresActualizarDto>
    {
        private readonly GoogleMapsService _googleMapsService;
        private readonly ColaboradorService _colaboradorService;
        private readonly SucursalService _sucursalService;
        private readonly IMapper _mapper;
        public readonly TranslogixDBContext _context;
        public SucursalColaboradorService(TranslogixDBContext translogixDBContext, 
                                          IMapper mapper, 
                                          GoogleMapsService googleMapsService, 
                                          ColaboradorService colaboradorService,
                                          SucursalService sucursalService)
            : base(translogixDBContext, mapper)
        {
            _googleMapsService = googleMapsService;
            _mapper = mapper;
            _colaboradorService = colaboradorService;
            _sucursalService = sucursalService;
            _context = translogixDBContext;
        }

    public async Task<ApiResponse<string>> InsertarAsync(SucursalesColaboradoresInsertarDto modelo)
    {
        var entidad = _mapper.Map<Sucursales_Colaboradores>(modelo);

        var resulColaboradores = _colaboradorService.ObtenerPorId(entidad.colaborador_id);
        var resulSucursales = _sucursalService.ObtenerPorId(entidad.sucursal_id);

        if (resulColaboradores.StatusCode != 400 && resulSucursales.StatusCode != 400)
        {
            var origen = new Location { Lat = resulColaboradores.Data.latitud, Lng = resulColaboradores.Data.longitud };
            var destino = new Location { Lat = resulSucursales.Data.latitud, Lng = resulSucursales.Data.longitud };

            var distancia = await _googleMapsService.CalcularDistanciaAsync(origen, destino);
            modelo.distancia_empleado_sucursal_km = (decimal)distancia;
            Insertar(modelo); // Asegúrate de que base.Insertar sea también asíncrono
        }

        return ApiResponseHelper.SuccessMessage("Registro guardado con éxito");
    }

        public ApiResponse<string> Insertar(SucursalesColaboradoresInsertarDto modelo)
        {
            try
            {
                var entidad = _mapper.Map<Sucursales_Colaboradores>(modelo);
                _context.Set<Sucursales_Colaboradores>().Add(entidad);
                _context.SaveChanges();

                return ApiResponseHelper.SuccessMessage("Registro guardado con éxito");
            }
            catch (Exception ex)
            {
                return ApiResponseHelper.Error("Error al guardar: " + ex.Message);
            }
        }


    }
}