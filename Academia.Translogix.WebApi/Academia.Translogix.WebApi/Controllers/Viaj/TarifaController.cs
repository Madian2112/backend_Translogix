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
    public class TarifaController : ControllerBase
    {
        private readonly TarifaService _tarifaService;

        public TarifaController(TarifaService tarifaService)
        {
            _tarifaService = tarifaService;
        }

        [HttpGet("ObtenerTarifas")]
        public IActionResult ObtenerTarifas()
        {
            var result = _tarifaService.ObtenerTodos();
            return Ok(result);
        }

        [HttpGet("ObtenerTarifas{id}")]
        public IActionResult ObtenerTarifa(int id)
        {
            var result = _tarifaService.ObtenerPorId(id);
            return Ok(result);
        }

        [HttpPost("InsertarTarifa")]
        public IActionResult InsertarTarifa([FromBody] TarifasDtoInsertar modelo)
        {
            var result = _tarifaService.Insertar(modelo);
            return Ok(result);
        }

        [HttpPut("ActualizarTarifa{id}")]
        public IActionResult ActualizarTarifa(int id, [FromBody] TarifasDtoActualizar modelo)
        {
            var result = _tarifaService.Actualizar(id, modelo);
            return Ok(result);
        }

        [HttpPatch("EliminadoLogico{id}/{prop}")]
        public IActionResult EliminadoLogico(int id, bool prop = false)
        {
            var result = _tarifaService.EliminadoLogico(id, prop);
            return Ok(result);
        } 
    }
}