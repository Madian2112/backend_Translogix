using Academia.Translogix.WebApi._Features.Viaj.Dtos;
using Academia.Translogix.WebApi.Common._ApiResponses;
using Academia.Translogix.WebApi.Common._BaseDomain;
using Academia.Translogix.WebApi.Infrastructure;
using Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase.Entities.Gral;
using Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase.Entities.Viaj;
using AutoMapper;
using Farsiman.Domain.Core.Standard.Repositories;
using Microsoft.EntityFrameworkCore;
using static Academia.Translogix.WebApi._Features.Viaj.Dtos.TransportistasDto;

namespace Academia.Translogix.WebApi._Features.Viaj.Services
{
    public class TransportistaService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TransportistaService(UnitOfWorkBuilder unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork.BuildDbTranslogix();
            _mapper = mapper;
        }

        public ApiResponse<List<TransportistaDto>> ObtenerTodos()
        {
            try
            {
                var lista = _unitOfWork.Repository<Transportistas>().AsQueryable().AsNoTracking().ToList();

                var listaDto = _mapper.Map<List<TransportistaDto>>(lista);

                return ApiResponseHelper.Success(listaDto, "Registros obtenidos con éxito");
            }
            catch (Exception ex)
            {
                var response = ApiResponseHelper.ErrorDto<List<TransportistaDto>>("Error al obtener registros: " + ex.Message);
                if (response.Data == null)
                {
                    response.Data = [];
                }
                return response;
            }
        }

        public ApiResponse<string> InserTransportistaPersona(TransportistaInsertarDto modelo)
        {
            var transportista = modelo.Transportista;
            var persona = modelo.Persona;
            persona.usuario_creacion = transportista.usuario_creacion;

            var transportistaNoNulos = BaseDomainHelpers.ValidarCamposNulosVacios(transportista);
            var personaNoNulo = BaseDomainHelpers.ValidarCamposNulosVacios(persona);

            if (!transportistaNoNulos.Success || !personaNoNulo.Success)
            {
                string mensajeValidacionColaborador = transportistaNoNulos.Success ? "" : transportistaNoNulos.Message;
                string mensajeValidacionPersona = personaNoNulo.Success ? "" : personaNoNulo.Message;
                return ApiResponseHelper.Error($"No se aceptan valores nulos: {mensajeValidacionColaborador} {mensajeValidacionPersona}");
            }

            try
            {

                var identidadIgual = (from perso in _unitOfWork.Repository<Personas>().AsQueryable().AsNoTracking()
                                      where perso.identidad == persona.identidad
                                      select perso).FirstOrDefault();

                if (identidadIgual != null)
                {
                    return ApiResponseHelper.Error("Ya existe un colaborador con la misma identidad");
                }

                _unitOfWork.BeginTransaction();

                var entidadPersona = _mapper.Map<Personas>(persona);

                _unitOfWork.Repository<Personas>().Add(entidadPersona);
                _unitOfWork.SaveChanges();

                transportista.persona_id = entidadPersona.persona_id;
                var entidadTransportista = _mapper.Map<Transportistas>(transportista);


                _unitOfWork.Repository<Transportistas>().Add(entidadTransportista);
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
    }
}
