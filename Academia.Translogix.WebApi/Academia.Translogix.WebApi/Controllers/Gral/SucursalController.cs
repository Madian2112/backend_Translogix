using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Academia.Translogix.WebApi._Features.Viaj;
using Academia.Translogix.WebApi._Features.Viaj.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Academia.Translogix.WebApi.Controllers.Viaj
{
    [ApiController]
    [Route("api/[controller]")]
    public class SucursalController : ControllerBase
    {
        private readonly SucursalService _sucursalService;

        public SucursalController(SucursalService sucursalService)
        {
            _sucursalService = sucursalService;
        }

        [HttpGet("ObtenerSucursales")]
        public IActionResult ObtenerSucursales()
        {
            var result = _sucursalService.ObtenerTodos();
            return Ok(result);
        }

        [HttpGet("ObtenerSucursales{id}")]
        public IActionResult ObtenerSucursal(int id)
        {
            var result = _sucursalService.ObtenerPorId(id);
            return Ok(result);
        }

        [HttpPost("InsertarSucursal")]
        public IActionResult InsertarSucursal([FromBody] SucursalesDtoInsertar modelo)
        {
            var result = _sucursalService.Insertar(modelo);
            return Ok(result);
        }

        [HttpPut("ActualizarSucursal{id}")]
        public IActionResult ActualizarSucursal(int id, [FromBody] SucursalesDtoActualizar modelo)
        {
            var result = _sucursalService.Actualizar(id, modelo);
            return Ok(result);
        }

        [HttpPatch("EliminadoLogico{id}/{prop}")]
        public IActionResult EliminadoLogico(int id, bool prop = false)
        {
            var result = _sucursalService.EliminadoLogico(id, prop);
            return Ok(result);
        }

        [HttpDelete("EliminarSucursal{id}")]
        public IActionResult EliminarSucursal(int id)
        {
            var result = _sucursalService.EliminarCompletamente(id);
            return Ok(result);
        }
    }
}