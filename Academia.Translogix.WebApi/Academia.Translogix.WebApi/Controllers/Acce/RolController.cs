using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Academia.Translogix.WebApi._Features.Viaj.Dtos;
using Academia.Translogix.WebApi._Features.Viaj.Services;
using Microsoft.AspNetCore.Mvc;

namespace Academia.Translogix.WebApi.Controllers.Viaj
{
    [ApiController]
    [Route("api/[controller]")]
    public class RolController : ControllerBase
    {
        private readonly RolService _rolService;

        public RolController(RolService rolService)
        {
            _rolService = rolService;
        }

        [HttpGet("ObtenerRoles")]
        public IActionResult ObtenerRoles()
        {
            var result = _rolService.ObtenerTodos();
            return Ok(result);
        }

        [HttpGet("ObtenerRoles{id}")]
        public IActionResult ObtenerRol(int id)
        {
            var result = _rolService.ObtenerPorId(id);
            return Ok(result);
        }

        [HttpPost("InsertarRol")]
        public IActionResult InsertarRol([FromBody] RolesDtoInsertar modelo)
        {
            var result = _rolService.Insertar(modelo);
            return Ok(result);
        }

        [HttpPut("ActualizarRol{id}")]
        public IActionResult ActualizarRol(int id, [FromBody] RolesDtoActualizar modelo)
        {
            var result = _rolService.Actualizar(id, modelo);
            return Ok(result);
        }

        [HttpPatch("EliminadoLogico{id}/{prop}")]
        public IActionResult EliminadoLogico(int id, bool prop = false)
        {
            var result = _rolService.EliminadoLogico(id, prop);
            return Ok(result);
        }
    }
}