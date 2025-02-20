using Academia.Translogix.WebApi._Features.Acce.Dtos;
using Academia.Translogix.WebApi.Infrastructure._ApiResponses;
using Academia.Translogix.WebApi.Infrastructure._BaseService;
using Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase;
using Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase.Entities.Acce;
using AutoMapper;
using System.Security.Cryptography;
using System.Text;

namespace Academia.Translogix.WebApi._Features.Acce.Services
{
    public class UsuarioService : BaseService<Usuarios, UsuariosDto, UsuariosDtoInsertar, UsuariosDtoActualizar>
    {

        public UsuarioService(TranslogixDBContext translogix, IMapper mapper)
            : base(translogix, mapper)
        {
        }

        private byte[] ConvertirClaveASha256(string clave)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                return sha256.ComputeHash(Encoding.UTF8.GetBytes(clave));
            }
        }

        public ApiResponse<string> Insertar(UsuariosDtoInsertar modelo)
        {
            modelo.clave = ConvertirClaveASha256(modelo.claveinput);
            return base.Insertar(modelo);
        }

        public ApiResponse<string> Actualizar(UsuariosDtoInsertar modelo)
        {
            modelo.clave = ConvertirClaveASha256(modelo.claveinput);
            return base.Actualizar(modelo);
        }

    }
}
