namespace Academia.Translogix.WebApi._Features.Acce.Requirement
{
    public class UsuariosDomainRequirement
    {
        public static UsuariosDomainRequirement Fill(
            bool rolExistente,
            bool colaboradorExistente
            ) => new UsuariosDomainRequirement
            {
                RolExistente = rolExistente,
                ColaboradorExistente = colaboradorExistente
            };

        public bool RolExistente { get; set; }
        public bool ColaboradorExistente { get; set; }

        public bool IsValid() => RolExistente && ColaboradorExistente;

        public string ObtenerMensajesError()
        {
            List<string> errors = new List<string>();

            if (!RolExistente)
                errors.Add("No se encontraron roles existentes.");
            if (!ColaboradorExistente)
                errors.Add("No se encontraron colaboradores existentes.");

            return string.Join(" ", errors);
        }

    }
}
