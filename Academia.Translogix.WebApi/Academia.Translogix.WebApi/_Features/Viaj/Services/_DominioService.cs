using System.Globalization;
using System.Text.RegularExpressions;
using Academia.Translogix.WebApi._Features.Gral.Dtos;
using Academia.Translogix.WebApi._Features.Gral.Requeriment;
using Academia.Translogix.WebApi._Features.Viaj.Requirement;
using Academia.Translogix.WebApi.Common._ApiResponses;
using Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase.Entities.Acce;
using Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase.Entities.Gral;
using Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase.Entities.Viaj;
using static Academia.Translogix.WebApi._Features.Viaj.Dtos.TransportistasDto;

namespace Academia.Translogix.WebApi._Features.Viaj.Services
{
    public class ViajeDominioService
    {
        public bool esNuloPersona<T>(T usuario) where T : class
        {
            return usuario == null? true : false;
        }

        public ApiResponse<Transportistas> CrearTransportista(Transportistas entidad, TransportistasDomainRequirement requirement)
        {

            if (!requirement.IsValid())
                return new ApiResponse<Transportistas>(
                success: false,
                message: requirement.ObtenerMensajesError(),
                data: null,
                statusCode: 404
                );

            return new ApiResponse<Transportistas>(
                success: true,
                message: "Dto de transportistas validado correctamente",
                data: entidad,
                statusCode: 200
            );
        }
    }
}
