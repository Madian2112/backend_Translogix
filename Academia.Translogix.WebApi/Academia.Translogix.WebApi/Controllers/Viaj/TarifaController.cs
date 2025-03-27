using Academia.Translogix.WebApi._Features.Viaj.Services;
using Microsoft.AspNetCore.Mvc;

namespace Academia.Translogix.WebApi.Controllers.Viaj
{
    [Route("api/[controller]")]
    [ApiController]
    public class TarifaController : ControllerBase
    {
        private readonly TarifaService _tarifaService;

        public TarifaController(TarifaService tarifaService)
        {
            _tarifaService = tarifaService;
        }

        [HttpGet("ObtenerTarifas")]
        public IActionResult ObtenerTarifas()
        {
            var result = _tarifaService.ObtenerTodos();
            return Ok(result);
        }
    }
}
