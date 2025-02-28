using Academia.Translogix.WebApi._Features.Gral.Dtos;
using Academia.Translogix.WebApi._Features.Gral.Services;
using Academia.Translogix.WebApi._Features.Viaj.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using static Academia.Translogix.WebApi._Features.Viaj.Dtos.TransportistasDto;

namespace Academia.Translogix.WebApi.Controllers.Viaj
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransportistaController : ControllerBase
    {
        private readonly TransportistaService _transportistaService;

        public TransportistaController(TransportistaService transportistaService)
        {
            _transportistaService = transportistaService;
        }

        [HttpGet("ObtenerTransportistas")]
        public IActionResult ObtenerTransportistas()
        {
            var result = _transportistaService.ObtenerTodos();
            return Ok(result);
        }

        [HttpPost("InsertarTranportista")]
        public  IActionResult InsertarTranportista(TransportistaInsertarDto modelo)
        {
            var result = _transportistaService.InserTransportistaPersona(modelo);
            return Ok(result);
        }
    }
}
