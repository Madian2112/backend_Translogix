namespace Academia.Translogix.WebApi._Features.Gral.Requirement
{
    public class PersonasDomainRequirement
    {
        public static PersonasDomainRequirement Fill(
            bool paisExitente) => new PersonasDomainRequirement
            {
                PaisExitente =  paisExitente
            };

        public bool PaisExitente { get; set; }

        public string ObtenerMensajesError()
        {
            var errors = new List<string>();

            if (!PaisExitente)
                errors.Add("No se encontraron paises existentes.");

            return string.Join(" ", errors);    
        }
    }
}
