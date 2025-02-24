using Academia.Translogix.WebApi.Common._ApiResponses;

namespace Academia.Translogix.WebApi.Common._BaseService
{
    public interface IBaseService<T, TDto, TDtoInsertar, TDtoActualizar>
        where T : class
    {
        ApiResponse<List<TDto>> ObtenerTodos();
        ApiResponse<TDto> ObtenerPorId(int id);
        ApiResponse<string> Insertar(TDtoInsertar modelo);
        ApiResponse<string> Actualizar(int id, TDtoActualizar modelo);
        ApiResponse<string> EliminadoLogico(int id, bool esActivo);
    }
}
