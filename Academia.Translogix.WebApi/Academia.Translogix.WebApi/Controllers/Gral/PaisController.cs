using Academia.Translogix.WebApi._Features.Gral.Dtos;
using Academia.Translogix.WebApi._Features.Gral.Services;
using Academia.Translogix.WebApi.Infrastructure._ApiResponses;
using Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase.Entities.Gral;
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
            return Ok(result);
        }

        [HttpGet("ObtenerPaises{id}")]
        public IActionResult ObtenerPais(int id)
        {
            var result = _paisService.ObtenerPorId(id);
            return Ok(result);
        }

        [HttpPost("InsertarPais")]
        public IActionResult InsertarPais([FromBody] PaisesDtoInsertar modelo)
        {
            var result = _paisService.Insertar(modelo);
            return Ok(result);
        }

        [HttpPut("ActualizarPais{id}")]
        public IActionResult ActualizarPais(int id, [FromBody] PaisesDtoActualizar modelo)
        {
            var result = _paisService.Actualizar(id, modelo);
            return Ok(result);
        }

        [HttpPatch("EliminadoLogico{id}/{prop}")]
        public IActionResult EliminadoLogico(int id, bool prop = false)
        {
            var result = _paisService.EliminadoLogico(id, prop);
            return Ok(result);
        }

        [HttpDelete("EliminarPais{id}")]
        public IActionResult EliminarPais(int id)
        {
            var result = _paisService.EliminarCompletamente(id);
            return Ok(result);
        }
    }
}
