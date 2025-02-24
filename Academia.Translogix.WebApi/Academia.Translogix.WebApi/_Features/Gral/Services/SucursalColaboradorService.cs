using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Academia.Translogix.WebApi._Features.Gral.Dtos;
using Academia.Translogix.WebApi._Features.Viaj;
using Academia.Translogix.WebApi._Features.Viaj.Dtos;
using Academia.Translogix.WebApi.Common._ApiResponses;
using Academia.Translogix.WebApi.Common._BaseService;
using Academia.Translogix.WebApi.Infrastructure;
using Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase;
using Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase.Entities.Gral;
using Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase.Entities.Viaj;
using AutoMapper;
using Farsiman.Domain.Core.Standard.Repositories;
using Microsoft.EntityFrameworkCore;
using static Academia.Translogix.WebApi._Features.Gral.Services.GoogleMapsService;

namespace Academia.Translogix.WebApi._Features.Gral.Services
{
    public class SucursalColaboradorService : BaseService<Sucursales_Colaboradores, SucursalesColaboradoresDto, SucursalesColaboradoresInsertarDto, SucursalesColaboradoresActualizarDto>
    {
        private readonly GoogleMapsService _googleMapsService;
        private readonly ColaboradorService _colaboradorService;
        private readonly SucursalService _sucursalService;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _context;

        public SucursalColaboradorService(IMapper mapper,
            UnitOfWorkBuilder unitOfWork,
                                          GoogleMapsService googleMapsService, 
                                          ColaboradorService colaboradorService,
                                          SucursalService sucursalService)
            : base(mapper, unitOfWork)
        {
            _googleMapsService = googleMapsService;
            _mapper = mapper;
            _colaboradorService = colaboradorService;
            _sucursalService = sucursalService;
            _context = unitOfWork.BuildDbTranslogix();
        }

        public async Task<ApiResponse<string>> InsertarAsync(SucursalesColaboradoresInsertarDto modelo)
        {
            var entidad = _mapper.Map<Sucursales_Colaboradores>(modelo);

            var resulColaboradores =  _context.Repository<Colaboradores>().AsQueryable().AsNoTracking()
                .FirstOrDefault(x => x.colaborador_id == entidad.colaborador_id);

            var resulSucursales = _context.Repository<Sucursales>().AsQueryable().AsNoTracking()
                .FirstOrDefault(x => x.sucursal_id == entidad.sucursal_id);

            if (resulColaboradores == null || resulSucursales == null)
            {
                return ApiResponseHelper.Error("Colaborador o sucursal no encontrados.");
            }

            try
            {
                var origen = new Location { Lat = resulColaboradores.latitud, Lng = resulColaboradores.longitud };
                var destino = new Location { Lat = resulSucursales.latitud, Lng = resulSucursales.longitud };

                var distancia = await _googleMapsService.CalcularDistanciaAsync(origen, destino);
                decimal distanciaKm = ConvertirDistanciaAKilometros(distancia);

                modelo.distancia_empleado_sucursal_km = distanciaKm;
                return await Insertar(modelo); // Esperamos y devolvemos el resultado
            }
            catch (Exception ex)
            {
                return ApiResponseHelper.Error("No se pudo guardar el registro: " + ex.Message);
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

        public async Task<ApiResponse<string>> Insertar(SucursalesColaboradoresInsertarDto modelo)
        {
            try
            {
                var entidad = _mapper.Map<Sucursales_Colaboradores>(modelo);
                _context.Repository<Sucursales_Colaboradores>().Add(entidad);
                await _context.SaveChangesAsync(); // Nota: Usé await aquí
                return ApiResponseHelper.SuccessMessage("Registro guardado con éxito");
            }
            catch (Exception ex)
            {
                return ApiResponseHelper.Error("Error al guardar: " + ex.Message);
            }
        }


    }
}