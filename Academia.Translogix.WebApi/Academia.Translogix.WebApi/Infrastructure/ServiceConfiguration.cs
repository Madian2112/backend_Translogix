using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Academia.Translogix.WebApi._Features.Acce.Services;
using Academia.Translogix.WebApi._Features.Gral.Services;
using Academia.Translogix.WebApi._Features.Viaj;

namespace Academia.Translogix.WebApi.Infrastructure
{
    public static  class ServiceConfiguration
    {
        public static void ConfiguracionServicios(IServiceCollection service)
        {
            service.AddScoped<UsuarioService>();
            service.AddScoped<MonedaService>();
            service.AddScoped<PaisService>();
            service.AddScoped<SucursalService>();
        }
    }
}