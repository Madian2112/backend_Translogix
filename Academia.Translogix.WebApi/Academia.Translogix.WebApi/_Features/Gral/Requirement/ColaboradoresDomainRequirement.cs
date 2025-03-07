using System.Collections.Generic;

namespace Academia.Translogix.WebApi._Features.Gral.Requeriment
{
    public class ColaboradoresDomainRequirement
    {
        public static ColaboradoresDomainRequirement Fill(
            bool identidadIgual,
            bool correoIgual,
            bool estadoCivilExistente,
            bool cargoExistente) => new ColaboradoresDomainRequirement
            {
                IdentidadIgual = identidadIgual, 
                CargoExistente = cargoExistente,
                CorreoIgual = correoIgual,
                EstadoCivilExistente = estadoCivilExistente
            };

        public bool IdentidadIgual { get; set; }
        public bool CorreoIgual { get; set; }
        public bool EstadoCivilExistente { get; set; }
        public bool CargoExistente { get; set; }

        public bool IsValid() => IdentidadIgual && CorreoIgual && EstadoCivilExistente && CargoExistente;

        public string ObtenerMensajesError()
        {
            List<string> errors = new List<string>();

            if (!IdentidadIgual)
                errors.Add("Ya existe un colaborador con la misma identidad.");
            if (!CorreoIgual)
                errors.Add("Ya existe un colaborador con el mismo correo.");
            if (!EstadoCivilExistente)
                errors.Add("No se encontraron estados civiles existentes.");
            if (!CargoExistente)
                errors.Add("No se encontraron cargos existentes.");

            return string.Join(" ", errors);
        }
    }
}
