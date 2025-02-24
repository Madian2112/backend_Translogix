using System.Collections.Generic;
using System.Net.Security;
using System.Text.Json;
using Academia.Translogix.WebApi.Common._ApiResponses;
using Academia.Translogix.WebApi.Common._BaseDomain;
using Academia.Translogix.WebApi.Infrastructure;
using Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase;
using Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase.Entities.Gral;
using AutoMapper;
using Farsiman.Domain.Core.Standard.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Academia.Translogix.WebApi.Common._BaseService
{
    public class BaseService<T, TDto, TDtoInsertar, TDtoActualizar> : IBaseService<T, TDto, TDtoInsertar, TDtoActualizar>
        where T : class
        where TDtoInsertar : class
        where TDtoActualizar : class
    {
        private readonly IMapper _mapper;
        private readonly UnitOfWorkBuilder _unitOfWorkBuilder;
        private readonly IUnitOfWork _context;
        public BaseService(IMapper mapper, UnitOfWorkBuilder unitOfWorkBuilder)
        {
            _mapper = mapper;
            _unitOfWorkBuilder = unitOfWorkBuilder;
            _context = _unitOfWorkBuilder.BuildDbTranslogix();
        }

        public ApiResponse<List<TDto>> ObtenerTodos()
        {
            try
            {
                var lista = _context.Repository<T>().AsQueryable().AsNoTracking().ToList();

                var listaDto = _mapper.Map<List<TDto>>(lista);

                return ApiResponseHelper.Success(listaDto, "Registros obtenidos con éxito");
            }
            catch (Exception ex)
            {
                var response = ApiResponseHelper.ErrorDto<List<TDto>>("Error al obtener registros: " + ex.Message);
                if (response.Data == null)
                {
                    response.Data = [];
                }
                return response;
            }
        }

        public ApiResponse<TDto> ObtenerPorId(int id)
        {

            try
            {
                string pkId = "";
                var properties = typeof(T).GetProperties();
                foreach (var property in properties)
                {
                    bool isKey = property.GetCustomAttributes(typeof(System.ComponentModel.DataAnnotations.KeyAttribute), false).Length > 0;
                    if (isKey)
                    {
                        pkId = property.Name;
                        break;
                    }
                }

                var registro = _context.Repository<T>().AsQueryable().AsNoTracking()
                            .FirstOrDefault(x => EF.Property<int>(x, pkId) == id);


                var registroDto = _mapper.Map<TDto>(registro);
                if (registro == null)
                {
                    return ApiResponseHelper.NotFound<TDto>("Registro no encontrado");
                }

                return ApiResponseHelper.Success(registroDto, "Registro encontrado");
            }
            catch (Exception ex)
            {
                return ApiResponseHelper.ErrorDto<TDto>($"Error al buscar registro:  {ex.Message}");
            }
        }

        public ApiResponse<string> Insertar(TDtoInsertar modelo)
        {

            try
            {
                var result = BaseDomainHelpers.ValidarCamposNulos(modelo);
                if(result.StatusCode != 200)
                {
                    return ApiResponseHelper.Error("No se aceptan valores nulos");
                }

                var entidad = _mapper.Map<T>(modelo);
                _context.Repository<T>().Add(entidad);
                _context.SaveChanges();

                return ApiResponseHelper.SuccessMessage("Registro guardado con éxito");
            }
            catch (Exception ex)
            {
                return ApiResponseHelper.Error("Error al guardar: " + ex.Message);
            }
        }

        public ApiResponse<string> Actualizar(int id, TDtoActualizar modelo)
        {
            var _context = _unitOfWorkBuilder.BuildDbTranslogix();

            try
            {
                var result = BaseDomainHelpers.ValidarCamposNulos(modelo);
                if (!(result.StatusCode == 2000))
                {
                    return ApiResponseHelper.Error("No se aceptan valores nulos");
                }

                var registroExistente = _context.Repository<T>().AsQueryable().Select(x => x.Equals(id));

                if (registroExistente == null)
                {
                    return ApiResponseHelper.NotFound<string>("Registro no encontrado");
                }

                _mapper.Map(modelo, registroExistente);
                _context.SaveChanges();

                return ApiResponseHelper.SuccessMessage("Registro actualizado con éxito");
            }
            catch (Exception ex)
            {
                return ApiResponseHelper.Error("Error al actualizar: " + ex.Message);
            }
        }

        public ApiResponse<string> EliminadoLogico(int id, bool esActivo = false)
        {
            var _context = _unitOfWorkBuilder.BuildDbTranslogix();

            try
            {
                var registro = _context.Repository<T>().AsQueryable().Select(x => x.Equals(id));

                if (registro == null)
                {
                    return ApiResponseHelper.NotFound<string>("Registro no encontrado");
                }

                var propiedadActivo = registro.GetType().GetProperty("es_activo");

                if (propiedadActivo == null)
                {
                    return ApiResponseHelper.Error("El registro no tiene la propiedad es_activo");
                }

                propiedadActivo.SetValue(registro, esActivo);
                _context.SaveChanges();

                return ApiResponseHelper.SuccessMessage("Registro eliminado con éxito");
            }
            catch (Exception ex)
            {
                return ApiResponseHelper.Error("Error al eliminar: " + ex.Message);
            }
        }
        public ApiResponse<string> EliminarCompletamente(int id)
        {
            var _context = _unitOfWorkBuilder.BuildDbTranslogix();

            try
            {
                var registro = _context.Repository<T>().AsQueryable().Select(x => x.Equals(id));

                if (registro == null)
                {
                    return ApiResponseHelper.NotFound<string>("Registro no encontrado");
                }

                //_context.Repository<T>().Remove(registro);
                _context.SaveChanges();

                return ApiResponseHelper.SuccessMessage("Registro eliminado completamente con éxito");
            }
            catch (Exception ex)
            {
                return ApiResponseHelper.Error("Error al eliminar completamente: " + ex.Message);
            }
        }

        public async Task<string> TranslateToSpanish(string text)
        {
            using HttpClient client = new HttpClient();
            // Construir la URL con el texto a traducir y el par de idiomas (en a es)
            string url = $"https://api.mymemory.translated.net/get?q={Uri.EscapeDataString(text)}&langpair=en|es";

            // Obtener la respuesta en formato JSON
            string json = await client.GetStringAsync(url);

            // Parsear el JSON y extraer la traducción
            using JsonDocument document = JsonDocument.Parse(json);
            string translatedText = document.RootElement
                                            .GetProperty("responseData")
                                            .GetProperty("translatedText")
                                            .GetString();

            return translatedText;
        }
    }
}
