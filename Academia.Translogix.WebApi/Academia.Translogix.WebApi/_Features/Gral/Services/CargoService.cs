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
    public class CargoService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CargoService(IMapper mapper, UnitOfWorkBuilder unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork.BuildDbTranslogix();
        }

        public ApiResponse<List<CargosDto>> ObtenerTodos()
        {
            List<CargosDto> data = new List<CargosDto>();
            try
            {
                var lista = _unitOfWork.Repository<Cargos>().AsQueryable().AsNoTracking().ToList();
                var listaDto = _mapper.Map<List<CargosDto>>(lista);
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

                var response = new ApiResponse<List<CargosDto>>(false, errorMessage, data, statusCode);
                if (response.Data == null)
                {
                    response.Data = [];
                }
                return response;
            }
        }
    }
}