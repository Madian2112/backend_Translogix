using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Academia.Translogix.WebApi._Features.Gral.Dtos;
using Academia.Translogix.WebApi._Features.Gral.Services;
using Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase.Entities.Gral;
using Microsoft.AspNetCore.Mvc;

namespace Academia.Translogix.WebApi.Controllers.Gral
{
    [ApiController]
    [Route("api/[controller]")]
    public class ColaboradorController : ControllerBase
    {
        private readonly ColaboradorService _colaboradorService;

        public ColaboradorController(ColaboradorService colaboradorService)
        {
            _colaboradorService = colaboradorService;
        }

        [HttpGet("ObtenerColaboradores")]
        public IActionResult ObtenerColaboradores()
        {
            var result = _colaboradorService.ObtenerTodos();
            return Ok(result);
        }

        //[HttpGet("ObtenerColaboradores{id}")]
        //public IActionResult ObtenerColaborador(int id)
        //{
        //    var result = _colaboradorService.ObtenerPorId(id);
        //    return Ok(result);
        //}

        [HttpGet("ObtenerColaboradoresSinViajeFiltradosPorSucursal")]
        public IActionResult ObtenerColaboradoresSinViajeFiltradosPorSucursal([FromQuery] int sucursal_id)
        {
            var result = _colaboradorService.ObtenerColaboradoresSinViajeFiltradosPorSucursal(sucursal_id);
            return Ok(result);
        }

        [HttpPost("InsertarColaborador")]
        public async Task<IActionResult> InsertarColaborador(ColaboradoresDtoInsertar modelo)
        {
            var result = _colaboradorService.InsertarColaboradorPersona(modelo);
            return Ok(result);
        }

        //[HttpPut("ActualizarColaborador{id}")]
        //public IActionResult ActualizarColaborador(int id, [FromBody] ColaboradoresDtoActualizar modelo)
        //{
        //    var result = _colaboradorService.Actualizar(id, modelo);
        //    return Ok(result);
        //}

        //[HttpPatch("EliminadoLogico{id}/{prop}")]
        //public IActionResult EliminadoLogico(int id, bool prop = false)
        //{
        //    var result = _colaboradorService.EliminadoLogico(id, prop);
        //    return Ok(result);
        //}

        //[HttpDelete("EliminarColaborador{id}")]
        //public IActionResult EliminarColaborador(int id)
        //{
        //    var result = _colaboradorService.EliminarCompletamente(id);
        //    return Ok(result);
        //}   
    }
}