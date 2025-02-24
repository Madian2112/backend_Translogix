using Academia.Translogix.WebApi._Features.Gral.Dtos;
using Academia.Translogix.WebApi._Features.Gral.Services;
using Microsoft.AspNetCore.Http;
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

        [HttpGet("ObtenerCargos{id}")]
        public IActionResult ObtenerCargo(int id)
        {
            var result = _cargoService.ObtenerPorId(id);
            return Ok(result);
        }

        [HttpPost("InsertarCargo")]
        public IActionResult InsertarCargo([FromBody] CargosDtoInsertar modelo)
        {
            var result = _cargoService.Insertar(modelo);
            return Ok(result);
        }

        [HttpPut("ActualizarCargo{id}")]
        public IActionResult ActualizarCargo(int id, [FromBody] CargosDtoActualizar modelo)
        {
            var result = _cargoService.Actualizar(id, modelo);
            return Ok(result);
        }

        [HttpPatch("EliminadoLogico{id}/{prop}")]
        public IActionResult EliminadoLogico(int id, bool prop = false)
        {
            var result = _cargoService.EliminadoLogico(id, prop);
            return Ok(result);
        }
    }
}
