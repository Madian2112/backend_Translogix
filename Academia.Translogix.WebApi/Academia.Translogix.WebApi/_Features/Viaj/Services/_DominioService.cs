using Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase.Entities.Acce;

namespace Academia.Translogix.WebApi._Features.Viaj.Services
{
    public class ViajeDominioService
    {
        public bool esAdmin(Usuarios usuario)
        {
            return usuario.es_admin;
        }

        public bool esNulo(Usuarios usuario)
        {
            return usuario == null? false : true;
        }
    }
}
