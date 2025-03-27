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
        public IActionResult ObtenerColaboradorPorSucursales([FromQuery] int sucursal_id)
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
    }
}