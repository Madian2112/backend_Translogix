using Academia.Translogix.WebApi._Features.Gral.Services;
using Microsoft.AspNetCore.Mvc;

namespace Academia.Translogix.WebApi.Controllers.Gral
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaisController : ControllerBase
    {
        private readonly PaisService _paisService;

        public PaisController(PaisService paisService)
        {
            _paisService = paisService;
        }

        [HttpGet("ObtenerPaises")]
        public IActionResult ObtenerPaises()
        {
            var result = _paisService.ObtenerTodos();
            return StatusCode(result.StatusCode, result);
        }
    }
}
