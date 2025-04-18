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
    public class SucursalColaboradorController : ControllerBase
    {
        private readonly SucursalColaboradorService _sucursalColaboradorService;

        public SucursalColaboradorController(SucursalColaboradorService sucursalColaboradorService)
        {
            _sucursalColaboradorService = sucursalColaboradorService;
        }

        [HttpGet("ObtenerSucursalColaboradores")]
        public IActionResult ObtenerSucursalColaboradores()
        {
            var result = _sucursalColaboradorService.ObtenerTodos();
            return Ok(result);
        }

        [HttpGet("ObtenerColaboradoresPorSucursales")]
        public IActionResult ObtenerColaboradorPorSucursales([FromQuery]int sucursal_id)
        {
            var result = _sucursalColaboradorService.ObtenerColaboradoresPorSucursales(sucursal_id);
            return Ok(result);
        }

        [HttpGet("ObtenerColaboradoresPorSucursalesNoAsignados")]
        public IActionResult ObtenerColaboradoresPorSucursalesNoAsignados([FromQuery] int sucursal_id)
        {
            var result = _sucursalColaboradorService.ObtenerColaboradoresPorSucursalesNoAsignados(sucursal_id);
            return Ok(result);
        }

        [HttpPost("InsertarSucursalColaborador")]
        public async Task<IActionResult> InsertarSucursalColaborador(SucursalesColaboradoresInsertarDto modelo)
        {
            var resultado = await _sucursalColaboradorService.InsertarAsync(modelo);
            //var resultado =  _sucursalColaboradorService.Insertar(modelo);
            return Ok(resultado);
        }

        [HttpPut("ActualizarSucursalColaborador{id}")]
        public IActionResult ActualizarSucursalColaborador(int id, [FromBody] SucursalesColaboradoresActualizarDto modelo)
        {
            var result = _sucursalColaboradorService.Actualizar(id, modelo);
            return Ok(result);
        }

        [HttpPatch("EliminadoLogico{id}/{prop}")]
        public IActionResult EliminadoLogico(int id, bool prop = false)
        {
            var result = _sucursalColaboradorService.EliminadoLogico(id, prop);
            return Ok(result);
        }

        [HttpDelete("EliminarSucursalColaborador{id}")]
        public IActionResult EliminarSucursalColaborador(int id)
        {
            var result = _sucursalColaboradorService.EliminarCompletamente(id);
            return Ok(result);
        }
    }
}