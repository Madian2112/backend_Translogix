using Academia.Translogix.WebApi._Features.Acce.Dtos;
using Academia.Translogix.WebApi._Features.Acce.Services;
using Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase.Entities.Acce;
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

        [HttpPost("InicioSesion")]
        public IActionResult InicioSesion(UsuarioInicioSesion modelo)
        {
            Usuarios registro = new Usuarios();
            var result = _usuarioService.InicioSesion(modelo, registro);
            return Ok(result);
        }


        [HttpPost("InsertarUsuario")]
        public IActionResult InsertarUsuario([FromBody] UsuariosDtoInsertar modelo)
        {
            var result = _usuarioService.Insertar(modelo);

            return Ok(result);
        }
    }
}
