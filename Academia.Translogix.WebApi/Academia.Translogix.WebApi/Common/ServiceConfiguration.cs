using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Academia.Translogix.WebApi._Features.Acce.Services;
using Academia.Translogix.WebApi._Features.Gral.Services;
using Academia.Translogix.WebApi._Features.Reportes.Service;
using Academia.Translogix.WebApi._Features.Viaj;
using Academia.Translogix.WebApi._Features.Viaj.Services;
using Academia.Translogix.WebApi.Common._BaseService;

namespace Academia.Translogix.WebApi.Common
{
    public static class ServiceConfiguration
    {
        public static void ConfiguracionServicios(IServiceCollection service)
        {
            service.AddTransient<UsuarioService>();
            service.AddTransient<MonedaService>();
            service.AddTransient<PaisService>();
            service.AddTransient<SucursalService>();
            service.AddTransient<SucursalColaboradorService>();
            service.AddTransient<ColaboradorService>();
            service.AddTransient<CargoService>();
            service.AddTransient<TarifaService>();
            service.AddTransient<EstadoCivilService>();
            service.AddTransient<RolService>();
            service.AddTransient<ViajeService>();
            service.AddTransient<ReporteService>();
            service.AddTransient<_OpenRouteService>();
            service.AddTransient<GeneralDominioService>();
            service.AddTransient<ReporteDominioService>();
            service.AddTransient<TransportistaService>(); 
            service.AddTransient<ViajeDominioService>();
            service.AddTransient<GoogleMapsService>();
        }
    }
}