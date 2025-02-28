using System.Runtime.CompilerServices;
using Academia.Translogix.WebApi._Features.Viaj.Dtos;
using Academia.Translogix.WebApi._Features.Viaj.Services;
using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using static Academia.Translogix.WebApi._Features.Gral.Services._OpenRouteService;
using static Academia.Translogix.WebApi._Features.Viaj.Dtos.ViajesDto;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Academia.Translogix.WebApi.Controllers.Viaj
{
    [Route("api/[controller]")]
    [ApiController]
    public class ViajeController : ControllerBase
    {
        private readonly ViajeService _viajeService;

        public ViajeController(ViajeService viajeService)
        {
            _viajeService = viajeService;
        }

        [HttpGet("ObtenerViajes")]
        public IActionResult ObtenerViajes()
        {
            var result =  _viajeService.ObtenerTodos();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpPost("InsertarViaje")]
        public async Task<IActionResult> InsertarViaje([FromBody] ViajesModeloInsertarDto request)
        {
            var result = await _viajeService.AgruparCrearRutas(request);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
