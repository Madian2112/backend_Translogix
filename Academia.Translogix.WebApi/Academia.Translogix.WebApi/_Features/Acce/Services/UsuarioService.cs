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
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;
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

                bool rolExistente = _commonService.EntidadExistente<Roles>(mappUsuario.rol_id);
                bool colaboradorExistente = _commonService.EntidadExistente<Colaboradores>(mappUsuario.colaborador_id);

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

        public ApiResponse<UsuariosDto> InicioSesion(UsuarioInicioSesion modelo)
        {
            try
            {
                byte[] claveHash = ConvertirClave(modelo.clave);

                Usuarios registro = _unitOfWork.Repository<Usuarios>().AsQueryable()
                    .Include(r => r.Rol)
                    .Include(c => c.Colaborador)
                    .ThenInclude(perso => perso.Persona)
                    .Include(c => c.Colaborador)
                    .ThenInclude(cargo => cargo.Cargo)
                    .FirstOrDefault(x => x.nombre == modelo.usuario && x.clave == claveHash);    

                if (registro == null)
                    return ApiResponseHelper.Unauthorized<UsuariosDto>(Mensajes._28_Usuario_Invalido);

                var registroDto = _mapper.Map<UsuariosDto>(registro);
                return ApiResponseHelper.Success(registroDto, Mensajes._02_Registros_Obtenidos);
            }
            catch (Exception ex)
            {
                int statusCode = 400; // Por defecto, Bad Request
                string errorMessage = Mensajes._03_Error_Registros_Obtenidos + ex.Message;

                // Ejemplo: Detectar errores de base de datos
                if (ex is Microsoft.Data.SqlClient.SqlException ||
                    ex.Message.Contains("timeout", StringComparison.OrdinalIgnoreCase) ||
                    ex.Message.Contains("database", StringComparison.OrdinalIgnoreCase))
                {
                    statusCode = 500; // Error de servidor para problemas de base de datos
                    errorMessage = "Error de base de datos: " + ex.Message;
                }

                var response = new ApiResponse<UsuariosDto>(false, errorMessage, default, statusCode);
                if (response.Data == null)
                {
                    response.Data = null;
                }
                return response;
            }
        }

    }
}
