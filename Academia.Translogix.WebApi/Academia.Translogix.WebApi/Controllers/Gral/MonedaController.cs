using Academia.Translogix.WebApi._Features.Gral.Dtos;
using Academia.Translogix.WebApi._Features.Gral.Services;
using Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase.Entities.Gral;
using Microsoft.AspNetCore.Http;
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
        public IActionResult ObtenerUsuarios()
        {
            var resultado = _monedaService.ObtenerMonedas();
            return Ok(resultado);
        }
    }
}
