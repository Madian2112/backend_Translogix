using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Academia.Translogix.WebApi._Features.Acce.Services;
using Academia.Translogix.WebApi._Features.Gral.Services;
using Academia.Translogix.WebApi._Features.Viaj;
using Academia.Translogix.WebApi._Features.Viaj.Services;

namespace Academia.Translogix.WebApi.Common
{
    public static class ServiceConfiguration
    {
        public static void ConfiguracionServicios(IServiceCollection service)
        {
            service.AddScoped<UsuarioService>();
            service.AddScoped<MonedaService>();
            service.AddScoped<PaisService>();
            service.AddScoped<SucursalService>();
            service.AddTransient<SucursalColaboradorService>();
            service.AddScoped<ColaboradorService>();
            service.AddScoped<CargoService>();
            service.AddScoped<TarifaService>();
            service.AddScoped<EstadoCivilService>();
            service.AddScoped<RolService>();
            service.AddScoped<GoogleMapsService>();
        }
    }
}