using Academia.Translogix.WebApi._Features.Acce.Dtos;
using Academia.Translogix.WebApi._Features.Acce.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Academia.Translogix.WebApi.Controllers.Acce
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioService _usuarioService;

        public UsuarioController(UsuarioService suarioService)
        {
            _usuarioService = suarioService;
        }

        [HttpGet("ObtenerUsuarios")]
        public IActionResult ObtenerUsuarios()
        {
            var result = _usuarioService.ObtenerTodos();
            return Ok(result);
        }

        [HttpGet("ObtenerUsuario{id}")]
        public string ObtenerUsuario(int id)
        {
            return "value";
        }

        [HttpPost("InsertarUsuario")]
        public IActionResult InsertarUsuario([FromBody] UsuariosDtoInsertar modelo)
        {
            var result = _usuarioService.Insertar(modelo);

            return Ok(result);
        }

        [HttpPut("ActualizarUsuario{id}")]
        public IActionResult ActualizarUsuario(int id, [FromBody] UsuariosDtoActualizar modelo)
        {
            var result = _usuarioService.Actualizar(id, modelo);

            return Ok(result);
        }

        [HttpDelete("EliminadoLogico{id}")]
        public void EliminadoLogico(int id)
        {
        }
    }
}
