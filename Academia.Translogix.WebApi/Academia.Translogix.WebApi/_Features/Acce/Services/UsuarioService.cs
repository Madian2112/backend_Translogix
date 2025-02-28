using Academia.Translogix.WebApi._Features.Acce.Dtos;
using Academia.Translogix.WebApi.Common._ApiResponses;
using Academia.Translogix.WebApi.Common._BaseDomain;
using Academia.Translogix.WebApi.Common._BaseService;
using Academia.Translogix.WebApi.Infrastructure;
using Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase;
using Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase.Entities.Acce;
using Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase.Entities.Gral;
using AutoMapper;
using Farsiman.Domain.Core.Standard;
using Farsiman.Domain.Core.Standard.Repositories;
using System.Security.Cryptography;
using System.Text;

namespace Academia.Translogix.WebApi._Features.Acce.Services
{
    public class UsuarioService : BaseService<Usuarios, UsuariosDto, UsuariosDtoInsertar, UsuariosDtoActualizar>
    {

        private readonly IMapper _mapper;
        private readonly UnitOfWorkBuilder _unitOfWorkBuilder;
        private readonly IUnitOfWork _unitOfWork;

        public UsuarioService(IMapper mapper, UnitOfWorkBuilder unitOfWorkBuilder)
            : base(mapper, unitOfWorkBuilder)
        {
            _mapper = mapper;
            _unitOfWorkBuilder = unitOfWorkBuilder;
            _unitOfWork = _unitOfWorkBuilder.BuildDbTranslogix();
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
            var usuarioNoNulos = BaseDomainHelpers.ValidarCamposNulosVacios(modelo);
            if (!usuarioNoNulos.Success)
            {
                return ApiResponseHelper.Error($"No se aceptan valores nulos: {usuarioNoNulos.Message}");
            }
            modelo.clave = ConvertirClave(modelo.claveinput);
            return base.Insertar(modelo);
        }

        public ApiResponse<string> InicioSesion(int id, UsuariosDtoActualizar modelo)
        {
            return base.Actualizar(id,modelo);
        }

        public ApiResponse<UsuariosDto> InicioSesion(string usuario, string clave)
        {
            try
            {
                byte[] claveHash = ConvertirClave(clave);

                var registro = _unitOfWork.Repository<Usuarios>().Where(x => x.nombre == usuario && x.clave == claveHash).FirstOrDefault();    

                if (registro == null)
                {
                    return ApiResponseHelper.NotFound<UsuariosDto>("Usuario no encontrado");
                }

                var registroDto = _mapper.Map<UsuariosDto>(registro);
                return ApiResponseHelper.Success(registroDto, "Usuario encontrado");
            }
            catch (Exception ex)
            {
                return ApiResponseHelper.ErrorDto<UsuariosDto>($"Error al buscar registro:  {ex.Message}");
            }
        }

    }
}
