using Academia.Translogix.WebApi._Features.Gral.Services;
using Microsoft.AspNetCore.Mvc;

namespace Academia.Translogix.WebApi.Controllers.Gral
{
    [Route("api/[controller]")]
    [ApiController]
    public class CargoController : ControllerBase
    {
        private readonly CargoService _cargoService;

        public CargoController(CargoService cargoService)
        {
            _cargoService = cargoService;
        }

        [HttpGet("ObtenerCargos")]
        public IActionResult ObtenerCargos()
        {
            var result = _cargoService.ObtenerTodos();
            return Ok(result);
        }
    }
}
