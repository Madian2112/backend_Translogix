using Academia.Translogix.WebApi._Features.Acce.Requirement;
using Academia.Translogix.WebApi.Common._ApiResponses;
using Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase.Entities.Acce;

namespace Academia.Translogix.WebApi._Features.Acce.Services
{
    public class AccesoDominioService
    {
        public ApiResponse<Usuarios> CrearUsuario(Usuarios entidad, UsuariosDomainRequirement requirement)
        {
            if (!requirement.IsValid())
                return new ApiResponse<Usuarios>(
                success: false,
                message: requirement.ObtenerMensajesError(),
                data: new Usuarios(),
                statusCode: 404
                );

            if (entidad.nombre.Length > 150)
            {
                return new ApiResponse<Usuarios>(
                    success: false,
                    message: "El campo nombre no debe tener mas de 70 caracteres.",
                    data: entidad,
                    statusCode: 400
                    );
            }

            return new ApiResponse<Usuarios>(
                success: true,
                message: "Usuario validado correctamente",
                data: entidad,
                statusCode: 200
                );
        }
    }
}
