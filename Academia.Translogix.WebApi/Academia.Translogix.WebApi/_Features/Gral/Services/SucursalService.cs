using Academia.Translogix.WebApi._Features.Viaj.Dtos;
using Academia.Translogix.WebApi.Common;
using Academia.Translogix.WebApi.Common._ApiResponses;
using Academia.Translogix.WebApi.Infrastructure;
using Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase.Entities.Viaj;
using AutoMapper;
using Farsiman.Domain.Core.Standard.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Academia.Translogix.WebApi._Features.Viaj
{
    public class SucursalService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public SucursalService(IMapper mapper, UnitOfWorkBuilder unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork.BuildDbTranslogix();
        }

        public ApiResponse<List<SucursalesDto>> ObtenerTodos()
        {
            List<SucursalesDto> data = new List<SucursalesDto>();
            try
            {
                var lista = _unitOfWork.Repository<Sucursales>().AsQueryable().AsNoTracking().ToList();
                var listaDto = _mapper.Map<List<SucursalesDto>>(lista);
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

                var response = new ApiResponse<List<SucursalesDto>>(false, errorMessage, data, statusCode);
                if (response.Data == null)
                {
                    response.Data = [];
                }
                return response;
            }
        }
        public ApiResponse<string> Insertar(SucursalesDtoInsertar modelo)
        {
            try
            {
                var entidad = _mapper.Map<Sucursales>(modelo);
                _unitOfWork.Repository<Sucursales>().Add(entidad);
                _unitOfWork.SaveChanges();
                return ApiResponseHelper.SuccessMessage(Mensajes._07_Registro_Guardado);
            }
            catch (Exception ex)
            {
                return ApiResponseHelper.Error(Mensajes._08_Error_Guardado + ex.Message);
            }
        }
    }
}