using Academia.Translogix.WebApi._Features.Gral.Dtos;
using Academia.Translogix.WebApi._Features.Gral.Services;
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

        [HttpGet("ObtenerColaboradoresSinViajeFiltradosPorSucursal")]
        public IActionResult ObtenerColaboradoresSinViajeFiltradosPorSucursal([FromQuery] int sucursal_id)
        {
            var result = _colaboradorService.ObtenerColaboradoresSinViajeFiltradosPorSucursal(sucursal_id);
            return Ok(result);
        }

        [HttpPost("InsertarColaborador")]
        public IActionResult InsertarColaborador(ColaboradoresDtoInsertar modelo)
        {
            var result = _colaboradorService.InsertarColaboradorPersona(modelo);
            return Ok(result);
        }
    }
}