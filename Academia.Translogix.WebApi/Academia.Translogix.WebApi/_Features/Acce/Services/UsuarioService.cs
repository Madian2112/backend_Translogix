using System.Security.Cryptography;
using System.Text;
using Academia.Translogix.WebApi._Features.Acce.Dtos;
using Academia.Translogix.WebApi._Features.Acce.Requirement;
using Academia.Translogix.WebApi.Common;
using Academia.Translogix.WebApi.Common._ApiResponses;
using Academia.Translogix.WebApi.Common._BaseDomain;
using Academia.Translogix.WebApi.Infrastructure;
using Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase.Entities.Acce;
using Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase.Entities.Gral;
using AutoMapper;
using Farsiman.Domain.Core.Standard.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Academia.Translogix.WebApi._Features.Acce.Services
{
    public class UsuarioService
    {

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly CommonService _commonService;
        private readonly AccesoDominioService _accesoDominioService;

        public UsuarioService(IMapper mapper, UnitOfWorkBuilder unitOfWorkBuilder, CommonService commonService,
                              AccesoDominioService accesoDominioService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWorkBuilder.BuildDbTranslogix();
            _commonService = commonService;
            _accesoDominioService = accesoDominioService;
        }

        private static byte[] ConvertirClave(string clave)
        {
            return SHA256.HashData(Encoding.UTF8.GetBytes(clave));
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
            catch (Exception ex)
            {
                return ApiResponseHelper.Error(Mensajes._15_Error_Operacion + ex);
            }
        }

        public ApiResponse<UsuariosDto> InicioSesion(UsuarioInicioSesion modelo, Usuarios? registro)
        {
            UsuariosDto data = new UsuariosDto();
            try
            {
                byte[] claveHash = ConvertirClave(modelo.clave);

                Usuarios? usuarios = _unitOfWork.Repository<Usuarios>().AsQueryable()
                                    .Include(r => r.Rol)
                                    .Include(c => c.Colaborador)
                                    .ThenInclude(perso => perso.Persona)
                                    .Include(c => c.Colaborador)
                                    .ThenInclude(cargo => cargo.Cargo)
                                    .FirstOrDefault(x => x.nombre == modelo.usuario && x.clave == claveHash);
                registro = usuarios;
                if (registro == null)
                    return ApiResponseHelper.Unauthorized(data, Mensajes._28_Usuario_Invalido);

                var registroDto = _mapper.Map<UsuariosDto>(registro);
                return ApiResponseHelper.Success(registroDto, Mensajes._02_Registros_Obtenidos);
            }
            catch (Exception ex)
            {
                int statusCode = 400;
                string errorMessage = Mensajes._03_Error_Registros_Obtenidos + ex.Message;

                if (ex is Microsoft.Data.SqlClient.SqlException ||
                    ex.Message.Contains("timeout", StringComparison.OrdinalIgnoreCase) ||
                    ex.Message.Contains("database", StringComparison.OrdinalIgnoreCase))
                {
                    statusCode = 500;
                    errorMessage = "Error de base de datos: " + ex.Message;
                }

                var response = new ApiResponse<UsuariosDto>(false, errorMessage, data, statusCode);
                if (response.Data == null)
                {
                    response.Data = data;
                }
                return response;
            }
        }

        public ApiResponse<List<UsuariosDto>> ObtenerTodos()
        {
            List<UsuariosDto> data = new List<UsuariosDto>();
            try
            {
                var lista = _unitOfWork.Repository<Usuarios>().AsQueryable().AsNoTracking().ToList();
                var listaDto = _mapper.Map<List<UsuariosDto>>(lista);
                return ApiResponseHelper.Success(listaDto, Mensajes._02_Registros_Obtenidos);
            }
            catch (Exception ex)
            {
                int statusCode = 400;
                string errorMessage = Mensajes._03_Error_Registros_Obtenidos + ex.Message;

                if (ex is Microsoft.Data.SqlClient.SqlException ||
                    ex.Message.Contains("timeout", StringComparison.OrdinalIgnoreCase) ||
                    ex.Message.Contains("database", StringComparison.OrdinalIgnoreCase))
                {
                    statusCode = 500;
                    errorMessage = "Error de base de datos: " + ex.Message;
                }

                var response = new ApiResponse<List<UsuariosDto>>(false, errorMessage, data, statusCode);
                if (response.Data == null)
                {
                    response.Data = [];
                }
                return response;
            }
        }

    }
}
