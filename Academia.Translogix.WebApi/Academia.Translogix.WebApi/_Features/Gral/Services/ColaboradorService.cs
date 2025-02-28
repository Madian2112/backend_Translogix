using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Academia.Translogix.WebApi._Features.Gral.Dtos;
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

        public ColaboradorService(UnitOfWorkBuilder unitOfWork , IMapper mapper, 
                                  TranslogixDBContext translogixDBContext, GoogleMapsService googleMapsService,
                                  GeneralDominioService generalDominioService)
        {
            _unitOfWork = unitOfWork.BuildDbTranslogix();
            _contextT = translogixDBContext;
            _mapper = mapper;
            _googleMapsService = googleMapsService;
            _generalDominioService = generalDominioService;
        }

        public ApiResponse<string> InsertarColaboradorPersona(ColaboradoresDtoInsertar modeloInsertar)
        {
            var colaborador = modeloInsertar.Colaborador;
            var persona = modeloInsertar.Persona;
            persona.usuario_creacion = colaborador.usuario_creacion;

            var colaboradorNoNulos = BaseDomainHelpers.ValidarCamposNulosVacios(colaborador);
            var personaNoNulo = BaseDomainHelpers.ValidarCamposNulosVacios(persona);

            if(!colaboradorNoNulos.Success || !personaNoNulo.Success)
            {
                string mensajeValidacionColaborador = colaboradorNoNulos.Success ? "" : colaboradorNoNulos.Message;
                string mensajeValidacionPersona = personaNoNulo.Success ? "" : personaNoNulo.Message;
                return ApiResponseHelper.Error($"No se aceptan valores nulos: {mensajeValidacionColaborador} {mensajeValidacionPersona}");
            }

            try
            {
                var identidadIgual = (from perso in _unitOfWork.Repository<Personas>().AsQueryable().AsNoTracking()
                                      where perso.identidad == persona.identidad
                                      select perso).FirstOrDefault();
                var validarNuloIdentidad = _generalDominioService.esNuloPersona(identidadIgual);

                if(!validarNuloIdentidad)
                {
                    return ApiResponseHelper.Error("Ya existe un colaborador con la misma identidad");
                }

                var correoIgual = (from perso in _unitOfWork.Repository<Personas>().AsQueryable().AsNoTracking()
                                      where perso.correo_electronico == persona.correo_electronico
                                      select perso).FirstOrDefault();
                var validarNuloCorreo = _generalDominioService.esNuloPersona(correoIgual);

                if (!validarNuloCorreo)
                {
                    return ApiResponseHelper.Error("Ya existe un colaborador con el mismo correo");
                }

                _unitOfWork.BeginTransaction();

                var entidadPersona = _mapper.Map<Personas>(persona);

                _unitOfWork.Repository<Personas>().Add(entidadPersona);
                _unitOfWork.SaveChanges();

                colaborador.persona_id = entidadPersona.persona_id;
                var entidadColaborador = _mapper.Map<Colaboradores>(colaborador);

                
                _unitOfWork.Repository<Colaboradores>().Add(entidadColaborador);
                _unitOfWork.SaveChanges();

                _unitOfWork.Commit();
                return ApiResponseHelper.SuccessMessage("Registro guardado con éxito");
            }

            catch(Exception ex)
            {
                _unitOfWork.RollBack();
                return ApiResponseHelper.Error("No se pudo realizar la operacion: " + ex);
            }
        }

        public ApiResponse<List<ColaboradoresSinViaje>> ObtenerColaboradoresSinViajeFiltradosPorSucursal(int sucursal_id)
        {
            try
            {
                var resulSucursales = (from su in _unitOfWork.Repository<Sucursales>().AsQueryable().AsNoTracking()
                                       where su.sucursal_id == sucursal_id
                                       select su).FirstOrDefault();
                if (resulSucursales == null)
                {
                    return ApiResponseHelper.ErrorDto<List<ColaboradoresSinViaje>>("No se encontro ninguna sucursal con ese Id");
                }

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


                return ApiResponseHelper.Success<List<ColaboradoresSinViaje>>(query, "Colaboradores obtenidos con exito");
            }

            catch
            {
                return ApiResponseHelper.ErrorDto<List<ColaboradoresSinViaje>>("Error realizar la operacion");
            }
        }

    }
}