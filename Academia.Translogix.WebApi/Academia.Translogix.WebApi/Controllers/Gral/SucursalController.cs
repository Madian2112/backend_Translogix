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

        [HttpPost("InsertarSucursal")]
        public IActionResult InsertarSucursal([FromBody] SucursalesDtoInsertar modelo)
        {
            var result = _sucursalService.Insertar(modelo);
            return Ok(result);
        }
    }
}