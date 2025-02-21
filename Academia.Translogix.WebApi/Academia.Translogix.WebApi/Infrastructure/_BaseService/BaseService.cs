using System.Collections.Generic;
using System.Net.Security;
using System.Text.Json;
using Academia.Translogix.WebApi.Infrastructure._ApiResponses;
using Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase;
using Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase.Entities.Gral;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Academia.Translogix.WebApi.Infrastructure._BaseService
{
    public class BaseService<T, TDto, TDtoInsertar, TDtoActualizar> : IBaseService<T, TDto, TDtoInsertar, TDtoActualizar>
        where T : class
    {
        private readonly TranslogixDBContext _context;
        private readonly IMapper _mapper;

        public BaseService(TranslogixDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public ApiResponse<List<TDto>> ObtenerTodos()
        {
            try
            {
                var lista = _context.Set<T>().AsQueryable().AsNoTracking().ToList();

                var listaDto = _mapper.Map<List<TDto>>(lista);

                return ApiResponseHelper.Success(listaDto, "Registros obtenidos con éxito");
            }
            catch (Exception ex)
            {
                var response = ApiResponseHelper.ErrorDto<List<TDto>>("Error al obtener registros: " + ex.Message);
                if(response.Data == null)
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
                var registro = _context.Set<T>().Find(id);

                if (registro == null)
                {
                    return ApiResponseHelper.NotFound<TDto>("Registro no encontrado");
                }

                var registroDto = _mapper.Map<TDto>(registro);
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
                
                var entidad = _mapper.Map<T>(modelo);
                _context.Set<T>().Add(entidad);
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
            try
            {
                var registroExistente = _context.Set<T>().Find(id);

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
            try
            {
                var registro = _context.Set<T>().Find(id);

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
            try
            {
                var registro = _context.Set<T>().Find(id);

                if (registro == null)
                {
                    return ApiResponseHelper.NotFound<string>("Registro no encontrado");
                }

                _context.Set<T>().Remove(registro);
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
