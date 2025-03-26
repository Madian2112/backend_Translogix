using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Academia.Translogix.WebApi._Features.Gral.Dtos;
using Academia.Translogix.WebApi._Features.Gral.Requeriment;
using Academia.Translogix.WebApi._Features.Gral.Requirement;
using Academia.Translogix.WebApi.Common;
using Academia.Translogix.WebApi.Common._ApiResponses;
using Academia.Translogix.WebApi.Common._BaseDomain;
using Academia.Translogix.WebApi.Common._BaseService;
using Academia.Translogix.WebApi.Infrastructure;
using Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase;
using Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase.Entities.Gral;
using Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase.Entities.Viaj;
using AutoMapper;
using Farsiman.Domain.Core.Standard.Repositories;
using Farsiman.Infraestructure.Core.Entity.Standard;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Identity.Client;
using static Academia.Translogix.WebApi._Features.Gral.Services.GoogleMapsService;

namespace Academia.Translogix.WebApi._Features.Gral.Services
{
    public class ColaboradorService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly TranslogixDBContext _contextT;
        private readonly IMapper _mapper;
        private readonly GoogleMapsService _googleMapsService;
        private readonly GeneralDominioService _generalDominioService;
        private readonly CommonService _commonService;

        public ColaboradorService(UnitOfWorkBuilder unitOfWork , IMapper mapper, 
                                  TranslogixDBContext translogixDBContext, GoogleMapsService googleMapsService,
                                  GeneralDominioService generalDominioService, CommonService commonService)
        {
            _unitOfWork = unitOfWork.BuildDbTranslogix();
            _contextT = translogixDBContext;
            _mapper = mapper;
            _googleMapsService = googleMapsService;
            _generalDominioService = generalDominioService;
            _commonService = commonService;
        }

        public ApiResponse<List<ColaboradoresDto>> ObtenerTodos()
        {
            try
            {
                List<Colaboradores>? lista = _unitOfWork.Repository<Colaboradores>().AsQueryable()
                    .Include(p => p.Persona)
                    .Include(c => c.Cargo)
                    .Include(estc => estc.EstadoCivil).ToList();

                var listaDto = _mapper.Map<List<ColaboradoresDto>>(lista);

                return ApiResponseHelper.Success(listaDto, Mensajes._02_Registros_Obtenidos);
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

                var response = new ApiResponse<List<ColaboradoresDto>>(false, errorMessage, default, statusCode);
                if (response.Data == null)
                {
                    response.Data = [];
                }
                return response;
            }
        }

        public ApiResponse<string> InsertarColaboradorPersona(ColaboradoresDtoInsertar modeloInsertar)
        {

            var colaboradorNoNulos = BaseDomainHelpers.ValidarCamposNulosVacios(modeloInsertar.Colaborador);
            var personaNoNulo = BaseDomainHelpers.ValidarCamposNulosVacios(modeloInsertar.Persona);

            if(!colaboradorNoNulos.Success || !personaNoNulo.Success)
            {
                string mensajeValidacionColaborador = colaboradorNoNulos.Success ? "" : colaboradorNoNulos.Message;
                string mensajeValidacionPersona = personaNoNulo.Success ? "" : personaNoNulo.Message;
                return ApiResponseHelper.Error($"{Mensajes._06_Valores_Nulos}{mensajeValidacionColaborador} {mensajeValidacionPersona}");
            }
           
            try
            {
                Personas mappPersona = _mapper.Map<Personas>(modeloInsertar.Persona);
                Colaboradores mappColaborador = _mapper.Map<Colaboradores>(modeloInsertar.Colaborador);

                bool identidadIgual = _commonService.IdentidadIgual(mappPersona.identidad);
                bool correoIgual = _commonService.CorreoIgual(mappPersona.correo_electronico);
                bool estadoCivilExistente = _commonService.EntidadExistente<Estados_Civiles>(mappColaborador.estado_civil_id);
                bool cargoExistente = _commonService.EntidadExistente<Cargos>(mappColaborador.cargo_id);
                bool paisExistente = _commonService.EntidadExistente<Paises>(mappPersona.pais_id);

                PersonasDomainRequirement domainreqPersona = PersonasDomainRequirement.Fill(paisExistente);
                ColaboradoresDomainRequirement domainreqColaborador = ColaboradoresDomainRequirement.Fill(identidadIgual, correoIgual, estadoCivilExistente, cargoExistente);

                var resulDomainPersona = _generalDominioService.CrearPersona(mappPersona, domainreqPersona);
                var resulDomainColaborador = _generalDominioService.CrearColaborador(mappColaborador, domainreqColaborador);


                if (!resulDomainPersona.Success)
                    return ApiResponseHelper.Error(resulDomainPersona.Message);

                if (!resulDomainColaborador.Success)
                    return ApiResponseHelper.Error(resulDomainColaborador.Message);


                Personas personas = resulDomainPersona.Data;
                Colaboradores colaboradores = resulDomainColaborador.Data;

                personas.usuario_creacion = colaboradores.usuario_creacion;
                personas.Colaboradores = new List<Colaboradores> { colaboradores };

                _unitOfWork.BeginTransaction();

                _unitOfWork.Repository<Personas>().Add(personas);
                _unitOfWork.SaveChanges();

                _unitOfWork.Commit();
                return ApiResponseHelper.SuccessMessage(Mensajes._07_Registro_Guardado);
            }

            catch(Exception ex)
            {
                _unitOfWork.RollBack();
                return ApiResponseHelper.Error(Mensajes._15_Error_Operacion + ex);
            }
        }

        public ApiResponse<List<ColaboradoresSinViaje>> ObtenerColaboradoresSinViajeFiltradosPorSucursal(int sucursal_id)
        {
            try
            {
                if (!_commonService.EntidadExistente<Sucursales>(sucursal_id))
                    return ApiResponseHelper.ErrorDto<List<ColaboradoresSinViaje>>(Mensajes._22_Sucursal_No_Encontrada);

                var query = (from co in _unitOfWork.Repository<Colaboradores>().AsQueryable()
                             join sucu in _unitOfWork.Repository<Sucursales_Colaboradores>().AsQueryable() on co.colaborador_id equals sucu.colaborador_id
                             join su in _unitOfWork.Repository<Sucursales>().AsQueryable() on sucu.sucursal_id equals su.sucursal_id
                             join perso in _unitOfWork.Repository<Personas>().AsQueryable() on co.persona_id equals perso.persona_id
                             where su.sucursal_id == sucursal_id
                             && !_unitOfWork.Repository<Viajes>().AsQueryable()
                                 .Join(_unitOfWork.Repository<Viajes_Detalles>().AsQueryable(),
                                       v => v.viaje_id,
                                       viaj => viaj.viaje_id,
                                       (v, viaj) => new { v, viaj })
                                 .Any(x => x.viaj.colaborador_id == co.colaborador_id
                                        && x.v.fecha.Date == DateTime.Today)
                             select new ColaboradoresSinViaje
                             {
                                 colaborador_id = co.colaborador_id,
                                 distancia_empleado_sucursal_km = sucu.distancia_empleado_sucursal_km,
                                 identidad = perso.identidad,
                                 latitud = co.latitud,
                                 longitud = co.longitud,
                                 primer_apellido = perso.primer_apellido,
                                 primer_nombre = perso.primer_nombre,
                                 segundo_apellido = perso.segundo_apellido,
                                 segundo_nombre = perso.segundo_nombre
                             }).ToList();


                return ApiResponseHelper.Success<List<ColaboradoresSinViaje>>(query, Mensajes._02_Registros_Obtenidos);
            }

            catch
            {
                return ApiResponseHelper.ErrorDto<List<ColaboradoresSinViaje>>(Mensajes._15_Error_Operacion);
            }
        }

    }
}