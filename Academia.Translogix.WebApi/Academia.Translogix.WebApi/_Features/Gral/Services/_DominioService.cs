using Academia.Translogix.WebApi._Features.Gral.Requeriment;
using Academia.Translogix.WebApi._Features.Gral.Requirement;
using Academia.Translogix.WebApi.Common._ApiResponses;
using Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase.Entities.Gral;

namespace Academia.Translogix.WebApi._Features.Gral.Services
{
    public class GeneralDominioService
    {
        public bool ValidarDistancia(decimal distancia)
        {
            return distancia > 50 || distancia == 0;
        }
        public ApiResponse<Personas> CrearPersona(Personas entidad, PersonasDomainRequirement requirement)
        {
            if (!requirement.PaisExitente)
                return new ApiResponse<Personas>(
                success: false,
                message: requirement.ObtenerMensajesError(),
                data: entidad,
                statusCode: 404
                );

            var listaErrorMensajes = new List<string>();
            string mensajesError = "";

            if (entidad.identidad.Trim().Length < 13)
                listaErrorMensajes.Add("El campo identidad debe tener al menos 13 digitos.");

            if (entidad.primer_nombre.Trim().Length > 70)
                listaErrorMensajes.Add("El campo primer nombre no debe tener mas de 70 caracteres.");

            if (entidad.segundo_nombre.Trim().Length > 70)
                listaErrorMensajes.Add("El campo segundo nombre no debe tener mas de 70 caracteres.");

            if (entidad.primer_apellido.Trim().Length > 70)
                listaErrorMensajes.Add("El campo segundo nombre no debe tener mas de 70 caracteres.");

            if (entidad.segundo_apellido.Trim().Length > 70)
                listaErrorMensajes.Add("El campo segundo nombre no debe tener mas de 70 caracteres.");

            if (entidad.sexo != 'M' && entidad.sexo != 'F')
                listaErrorMensajes.Add("El campo sexo solo puede ser masculino o femenino.");


            if (entidad.telefono.Trim().Length < 8)
                listaErrorMensajes.Add("El campo telefono debe tener minimo 8 digitos.");


            if (entidad.correo_electronico.Trim().Length > 250)
                listaErrorMensajes.Add("El campo correo electronico no debe tener mas de 250 caracteres.");


            if (listaErrorMensajes.Count > 0)
            {
                mensajesError = string.Join("\n ", listaErrorMensajes);
            }

            return new ApiResponse<Personas>(
                success: listaErrorMensajes.Count > 0,
                message: listaErrorMensajes.Count > 0 ? mensajesError : "Persona validado correctamente",
                data: entidad,
                statusCode: listaErrorMensajes.Count > 0 ? 406 : 200
            );
        }

        public ApiResponse<Colaboradores> CrearColaborador(Colaboradores entidad, ColaboradoresDomainRequirement requirement)
        {

            if (!requirement.IsValid())
                return new ApiResponse<Colaboradores>(
                success: false,
                message: requirement.ObtenerMensajesError(),
                data: entidad,
                statusCode: 404
                );

            List<string> listaErrorMensajes = new List<string>();
            string mensajesError = "";

            if (entidad.latitud == 0)
                listaErrorMensajes.Add("La latitud no debe ser 0.");


            if (entidad.longitud == 0)
                listaErrorMensajes.Add("La longitud no debe ser 0.");


            if (listaErrorMensajes.Count > 0)
            {
                mensajesError = string.Join("\n ", listaErrorMensajes);
            }

            return new ApiResponse<Colaboradores>(
                success: listaErrorMensajes.Count > 0,
                message: listaErrorMensajes.Count > 0 ? mensajesError : "Dto de colaborador validado correctamente",
                data: entidad,
                statusCode: listaErrorMensajes.Count > 0 ? 406 : 200
            );
        }

    }
}
