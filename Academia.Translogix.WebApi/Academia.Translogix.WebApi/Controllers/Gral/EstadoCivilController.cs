using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Academia.Translogix.WebApi._Features.Gral.Dtos;
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

        [HttpGet("ObtenerEstadosCiviles{id}")]
        public IActionResult ObtenerEstadoCivil(int id)
        {
            var result = _estadocivilService.ObtenerPorId(id);
            return Ok(result);
        }

        [HttpPost("InsertarEstadoCivil")]
        public IActionResult InsertarEstadoCivil([FromBody] EstadosCivilesDtoInsertar modelo)
        {
            var result = _estadocivilService.Insertar(modelo);
            return Ok(result);
        }

        [HttpPut("ActualizarEstadoCivil{id}")]
        public IActionResult ActualizarEstadoCivil(int id, [FromBody] EstadosCivilesDtoActualizar modelo)
        {
            var result = _estadocivilService.Actualizar(id, modelo);
            return Ok(result);
        }

        [HttpPatch("EliminadoLogico{id}/{prop}")]
        public IActionResult EliminadoLogico(int id, bool prop = false)
        {
            var result = _estadocivilService.EliminadoLogico(id, prop);
            return Ok(result);
        }
    }
}