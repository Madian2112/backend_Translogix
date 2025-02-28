using Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase.Entities.Gral;
using Microsoft.Extensions.Logging.Abstractions;

namespace Academia.Translogix.WebApi._Features.Gral.Services
{
    public class GeneralDominioService
    {
        public bool ValidarDistancia(decimal distancia)
        {
            return distancia > 50 || distancia == 0 ? false : true;
        }

        public bool esNuloPersona(Personas persona)
        {
            return persona == null ? true : false;
        }
    }
}
