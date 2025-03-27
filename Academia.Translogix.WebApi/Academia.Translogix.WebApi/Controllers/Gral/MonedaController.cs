using Academia.Translogix.WebApi._Features.Gral.Services;
using Microsoft.AspNetCore.Mvc;

namespace Academia.Translogix.WebApi.Controllers.Gral
{
    [Route("api/[controller]")]
    [ApiController]
    public class MonedaController : ControllerBase
    {
        private readonly MonedaService _monedaService;

        public MonedaController(MonedaService generalService)
        {
            _monedaService = generalService;
        }

        [HttpGet("ObtenerMonedas")]
        public IActionResult ObtenerMonedas()
        {
            var resultado = _monedaService.ObtenerTodos();
            return Ok(resultado);
        }
    }
}
