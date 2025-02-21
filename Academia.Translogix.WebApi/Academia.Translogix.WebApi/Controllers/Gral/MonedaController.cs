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
        public IActionResult ObtenerMonedas()
        {
            var resultado = _monedaService.ObtenerTodos();
            return Ok(resultado);
        }
        
        [HttpGet("ObtenerMoneda{id}")]
        public IActionResult ObtenerMoneda(int id)
        {
            var result = _monedaService.ObtenerPorId(id);
            return Ok(result);
        }

        [HttpPost("InsertarMoneda")]
        public IActionResult InsertarMoneda([FromBody] MonedasDtoInsertar modelo)
        {
            var result = _monedaService.Insertar(modelo);
            return Ok(result);
        }

        [HttpPut("ActualizarMoneda{id}")]
        public IActionResult ActualizarMoneda(int id, [FromBody] MonedasDtoActualizar modelo)
        {
            var result = _monedaService.Actualizar(id, modelo);
            return Ok(result);
        }

        [HttpPatch("EliminadoLogico{id}/{prop}")]
        public IActionResult EliminadoLogico(int id, bool prop = false)
        {
            var result = _monedaService.EliminadoLogico(id, prop);
            return Ok(result);
        }

        [HttpDelete("EliminarMoneda{id}")]
        public IActionResult EliminarMoneda(int id)
        {
            var result = _monedaService.EliminarCompletamente(id);
            return Ok(result);
        }
    }
}
