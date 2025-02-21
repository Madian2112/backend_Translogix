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

        private readonly TranslogixDBContext _context;
        private readonly IMapper _mapper;

        public UsuarioService(TranslogixDBContext translogix, IMapper mapper)
            : base(translogix, mapper)
        {
            _context = translogix;
            _mapper = mapper;
        }

        private byte[] ConvertirClave(string clave)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                return sha256.ComputeHash(Encoding.UTF8.GetBytes(clave));
            }
        }

        public ApiResponse<string> Insertar(UsuariosDtoInsertar modelo)
        {
            modelo.clave = ConvertirClave(modelo.claveinput);
            return base.Insertar(modelo);
        }

        public ApiResponse<string> InicioSesion(int id, UsuariosDtoActualizar modelo)
        {
            return base.Actualizar(id,modelo);
        }

        public ApiResponse<UsuariosDto> InicioSesion(int id, string clave)
        {
            try
            {
                byte[] claveHash = ConvertirClave(clave);

                var registro = _context.Set<Usuarios>().Where(x => x.usuario_id == id && x.clave == claveHash).FirstOrDefault();    

                if (registro == null)
                {
                    return ApiResponseHelper.NotFound<UsuariosDto>("Registro no encontrado");
                }

                var registroDto = _mapper.Map<UsuariosDto>(registro);
                return ApiResponseHelper.Success(registroDto, "Registro encontrado");
            }
            catch (Exception ex)
            {
                return ApiResponseHelper.ErrorDto<UsuariosDto>($"Error al buscar registro:  {ex.Message}");
            }
        }

    }
}
