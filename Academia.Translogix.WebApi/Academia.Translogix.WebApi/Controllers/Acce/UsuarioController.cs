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
        public IActionResult ObtenerUsuario(int id)
        {
            var result = _usuarioService.ObtenerPorId(id);
            return Ok(result);
        }

        [HttpGet("InicioSesion")]
        public IActionResult InicioSesion([FromQuery] string usuario, [FromQuery] string clave)
        {
            var result = _usuarioService.InicioSesion(usuario, clave);
            return Ok(result);
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

        [HttpPatch("EliminadoLogico{id}")]
        public IActionResult EliminadoLogico(int id)
        {
            var result = _usuarioService.EliminadoLogico(id);
            return Ok(result);
        }

        [HttpDelete("EliminadoNormal{id}")]
        public IActionResult EliminadoNormal(int id)
        {
            var result = _usuarioService.EliminarCompletamente(id);
            return Ok(result);
        }
    }
}
