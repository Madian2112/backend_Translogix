using Academia.Translogix.WebApi._Features.Gral.Dtos;
using Academia.Translogix.WebApi.Common;
using Academia.Translogix.WebApi.Common._ApiResponses;
using Academia.Translogix.WebApi.Infrastructure;
using Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase.Entities.Gral;
using AutoMapper;
using Farsiman.Domain.Core.Standard.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Academia.Translogix.WebApi._Features.Gral.Services
{
    public class MonedaService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public MonedaService(IMapper mapper, UnitOfWorkBuilder unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork.BuildDbTranslogix();
        }

        public ApiResponse<List<MonedasDto>> ObtenerTodos()
        {
            List<MonedasDto> data = new List<MonedasDto>();
            try
            {
                var lista = _unitOfWork.Repository<Monedas>().AsQueryable().AsNoTracking().ToList();
                var listaDto = _mapper.Map<List<MonedasDto>>(lista);
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

                var response = new ApiResponse<List<MonedasDto>>(false, errorMessage, data, statusCode);
                if (response.Data == null)
                {
                    response.Data = [];
                }
                return response;
            }
        }
    }
}