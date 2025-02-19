using Academia.Translogix.WebApi._Features.Gral.Dtos;
using Academia.Translogix.WebApi._Features.Gral.Services;
using Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase.Entities.Gral;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Academia.Translogix.WebApi.Controllers.Gral
{
    [Route("api/[controller]")]
    [ApiController]
    public class MonedaController : ControllerBase
    {
        private readonly GeneralService _generalService;

        public MonedaController(GeneralService generalService)
        {
            _generalService = generalService;
        }

        [HttpPost("ObtenerMonedas")]
        public IActionResult ObtenerUsuarios([FromBody] PaisesDto dto)
        {
            var resultado = _generalService.InsertarPais(dto);
            return Ok(resultado);
        }
    }
}
