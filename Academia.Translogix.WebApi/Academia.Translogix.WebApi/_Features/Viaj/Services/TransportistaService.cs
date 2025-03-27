using Academia.Translogix.WebApi._Features.Gral.Requirement;
using Academia.Translogix.WebApi._Features.Gral.Services;
using Academia.Translogix.WebApi._Features.Viaj.Requirement;
using Academia.Translogix.WebApi.Common;
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
        private readonly CommonService _commonService;
        private readonly GeneralDominioService _generalDominioService;
        private readonly ViajeDominioService _viajeDominioService;

        public TransportistaService(UnitOfWorkBuilder unitOfWork, IMapper mapper, CommonService commonService,
                                    GeneralDominioService generalDominioService, ViajeDominioService viajeDominioService)
        {
            _unitOfWork = unitOfWork.BuildDbTranslogix();
            _mapper = mapper;
            _commonService = commonService;
            _generalDominioService = generalDominioService;
            _viajeDominioService = viajeDominioService;
        }

        public ApiResponse<List<TransportistaDto>> ObtenerTodos()
        {
            List<TransportistaDto> data = new List<TransportistaDto>();
            try
            {
                var lista = _unitOfWork.Repository<Transportistas>().AsQueryable().AsNoTracking()
                    .Include(p => p.Persona)
                    .Include(t => t.Tarifa);

                var listaDto = _mapper.Map<List<TransportistaDto>>(lista);

                return ApiResponseHelper.Success(listaDto, Mensajes._02_Registros_Obtenidos);
            }
            catch (Exception ex)
            {
                var response = ApiResponseHelper.ErrorDto(data, Mensajes._03_Error_Registros_Obtenidos + ex.Message);
                if (response.Data == null)
                {
                    response.Data = [];
                }
                return response;
            }
        }

        public ApiResponse<string> InserTransportistaPersona(TransportistaInsertarDto modelo)
        {
            var transportistaNoNulos = BaseDomainHelpers.ValidarCamposNulosVacios(modelo.Transportista);
            var personaNoNulo = BaseDomainHelpers.ValidarCamposNulosVacios(modelo.Persona);

            if (!transportistaNoNulos.Success || !personaNoNulo.Success)
            {
                string mensajeValidacionColaborador = transportistaNoNulos.Success ? "" : transportistaNoNulos.Message;
                string mensajeValidacionPersona = personaNoNulo.Success ? "" : personaNoNulo.Message;
                return ApiResponseHelper.Error($"{Mensajes._06_Valores_Nulos} {mensajeValidacionColaborador} {mensajeValidacionPersona}");
            }

            try
            {
                Transportistas mappTransportista = _mapper.Map<Transportistas>(modelo.Transportista);
                Personas mappPersona = _mapper.Map<Personas>(modelo.Persona);



                bool identidadIgual = _commonService.IdentidadIgual(mappPersona.identidad);
                bool monedaExistente = _commonService.EntidadExistente<Monedas>(mappTransportista.moneda_id);
                bool tarifaExistente = _commonService.EntidadExistente<Tarifas>(mappTransportista.tarifa_id);
                bool paisExistente = _commonService.EntidadExistente<Paises>(mappPersona.pais_id);

                PersonasDomainRequirement domainreqPersona = PersonasDomainRequirement.Fill(paisExistente);
                TransportistasDomainRequirement domainreqTransportista = TransportistasDomainRequirement.Fill(identidadIgual, tarifaExistente, monedaExistente);

                var resulDomainPersona = _generalDominioService.CrearPersona(mappPersona, domainreqPersona);
                var resultDomainTransportista = _viajeDominioService.CrearTransportista(mappTransportista, domainreqTransportista);

                if (!resulDomainPersona.Success)
                    return ApiResponseHelper.Error(resulDomainPersona.Message);

                if (!resultDomainTransportista.Success)
                    return ApiResponseHelper.Error(resultDomainTransportista.Message);

                Personas personas = resulDomainPersona.Data;
                Transportistas transportistas = resultDomainTransportista.Data;

                personas.usuario_creacion = transportistas.usuario_creacion;
                personas.Transportistas = new List<Transportistas> { transportistas };

                _unitOfWork.BeginTransaction();

                _unitOfWork.Repository<Personas>().Add(personas);
                _unitOfWork.SaveChanges();

                _unitOfWork.Commit();
                return ApiResponseHelper.SuccessMessage(Mensajes._07_Registro_Guardado);
            }

            catch (Exception ex)
            {
                _unitOfWork.RollBack();
                return ApiResponseHelper.Error(Mensajes._15_Error_Operacion + ex);
            }
        }
    }
}
