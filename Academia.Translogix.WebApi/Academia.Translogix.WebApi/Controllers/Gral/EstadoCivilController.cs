using Academia.Translogix.WebApi._Features.Gral.Services;
using Microsoft.AspNetCore.Mvc;

namespace Academia.Translogix.WebApi.Controllers.Gral
{
    [ApiController]
    [Route("api/[controller]")]
    public class EstadoCivilController : ControllerBase
    {
        private readonly EstadoCivilService _estadocivilService;

        public EstadoCivilController(EstadoCivilService estadocivilService)
        {
            _estadocivilService = estadocivilService;
        }

        [HttpGet("ObtenerEstadosCiviles")]
        public IActionResult ObtenerEstadosCiviles()
        {
            var result = _estadocivilService.ObtenerTodos();
            return Ok(result);
        }
    }
}