using Academia.Translogix.WebApi._Features.Gral.Dtos;
using Academia.Translogix.WebApi._Features.Gral.Services;
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
        public async Task<IActionResult> ObtenerPaises()
        {
            var result = _paisService.ObtenerTodos();
            return StatusCode(result.StatusCode,result);
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
            return StatusCode(result.StatusCode, result);
        }
    }
}
