namespace Academia.Translogix.WebApi._Features.Viaj.Requirement
{
    public class TransportistasDomainRequirement
    {
        public static TransportistasDomainRequirement Fill(
            bool identidadIgual,
            bool tarifaExistente,
            bool monedaExistente) => new TransportistasDomainRequirement
            {
                IdentidadIgual = identidadIgual,
                TarifaExistente = tarifaExistente,
                MonedaExistente = monedaExistente
            };

        public bool IdentidadIgual { get; set; }
        public bool TarifaExistente { get; set; }
        public bool MonedaExistente { get; set; }

        public bool IsValid() => IdentidadIgual && TarifaExistente && MonedaExistente;

        public string ObtenerMensajesError()
        {
            var errors = new List<string>();

            if (!IdentidadIgual)
                errors.Add("Ya existe un colaborador con la misma identidad.");
            if (!TarifaExistente)
                errors.Add("No se encontraron tarifas existentes.");
            if (!MonedaExistente)
                errors.Add("No se encontraron monedas existentes.");

            return string.Join(" ", errors);
        }

    }
}
