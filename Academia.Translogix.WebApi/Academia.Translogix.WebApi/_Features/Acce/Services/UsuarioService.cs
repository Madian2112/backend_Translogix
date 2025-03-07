using Academia.Translogix.WebApi._Features.Acce.Dtos;
using Academia.Translogix.WebApi._Features.Acce.Requirement;
using Academia.Translogix.WebApi.Common;
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
        private readonly CommonService _commonService;
        private readonly AccesoDominioService _accesoDominioService;

        public UsuarioService(IMapper mapper, UnitOfWorkBuilder unitOfWorkBuilder, CommonService commonService,
                              AccesoDominioService accesoDominioService)
            : base(mapper, unitOfWorkBuilder)
        {
            _mapper = mapper;
            _unitOfWorkBuilder = unitOfWorkBuilder;
            _unitOfWork = _unitOfWorkBuilder.BuildDbTranslogix();
            _commonService = commonService;
            _accesoDominioService = accesoDominioService;
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
                return ApiResponseHelper.Error($"{Mensajes._06_Valores_Nulos}{usuarioNoNulos.Message}");

            try
            {
                modelo.clave = ConvertirClave(modelo.claveinput);

                Usuarios mappUsuario = _mapper.Map<Usuarios>(modelo);

                bool rolExistente = _commonService.EntidadExistente<Usuarios>(mappUsuario.rol_id);
                bool colaboradorExistente = _commonService.EntidadExistente<Usuarios>(mappUsuario.colaborador_id);

                UsuariosDomainRequirement domainreqUsuarios = UsuariosDomainRequirement.Fill(rolExistente, colaboradorExistente);

                var resultDomainUsuarios = _accesoDominioService.CrearUsuario(mappUsuario, domainreqUsuarios);

                if (!resultDomainUsuarios.Success)
                    return ApiResponseHelper.Error(resultDomainUsuarios.Message);

                Usuarios usuarios = resultDomainUsuarios.Data;

                _unitOfWork.Repository<Usuarios>().Add(usuarios);
                _unitOfWork.SaveChanges();

                _unitOfWork.Commit();
                return ApiResponseHelper.SuccessMessage(Mensajes._07_Registro_Guardado);
            }
            catch(Exception ex)
            {
                return ApiResponseHelper.Error(Mensajes._15_Error_Operacion + ex);

            }
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

                bool registro = _unitOfWork.Repository<Usuarios>().AsQueryable().Any(x => x.nombre == usuario && x.clave == claveHash);    

                if (!registro)
                    return ApiResponseHelper.NotFound<UsuariosDto>(Mensajes._04_Registros_No_Encontrado);

                var registroDto = _mapper.Map<UsuariosDto>(registro);
                return ApiResponseHelper.Success(registroDto, Mensajes._02_Registros_Obtenidos);
            }
            catch (Exception ex)
            {
                return ApiResponseHelper.ErrorDto<UsuariosDto>($"{Mensajes._14_Error_Buscar_Registro}{ex.Message}");
            }
        }

    }
}
