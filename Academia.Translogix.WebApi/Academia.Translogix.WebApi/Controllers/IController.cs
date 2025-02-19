using Microsoft.AspNetCore.Mvc;

namespace Academia.Translogix.WebApi.Controllers
{
    public interface IController<T>
    {
        [HttpGet("ObtenerMonedas")]
        IEnumerable<T> GetAllProducts();

        [HttpGet("ObtenerMonedas")]
        T GetProductById(int id);

        [HttpGet("ObtenerMonedas")]
        void AddProduct(T product);

        [HttpGet("ObtenerMonedas")]
        void UpdateProduct(T product);

        [HttpGet("ObtenerMonedas")]
        void DeleteProduct(int id);
    }
}
