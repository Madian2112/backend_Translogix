using Academia.Translogix.WebApi._Features.Reportes.Dtos;
using Academia.Translogix.WebApi._Features.Reportes.Service;
using Microsoft.AspNetCore.Mvc;

namespace Academia.Translogix.WebApi.Controllers.Reporte
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReporteController : ControllerBase
    {
        private readonly ReporteService _reporteService;
        public ReporteController(ReporteService reporteService)
        {
            _reporteService = reporteService;
        }

        [HttpPost("ObtenerReporte")]
        public IActionResult ObtenerReporte([FromBody] ReporteInsertarDto reporteInsertar)
        {
            var result = _reporteService.CargarReporte(reporteInsertar);
            return Ok(result);
        }
    }
}
