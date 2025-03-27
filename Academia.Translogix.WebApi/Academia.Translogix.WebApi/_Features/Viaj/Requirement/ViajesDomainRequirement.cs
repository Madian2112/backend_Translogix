namespace Academia.Translogix.WebApi._Features.Viaj.Requirement
{
    public class ViajesDomainRequirement
    {
        public static ViajesDomainRequirement Fill(
            bool sucursalExistente,
            bool transportistaExistente) => new ViajesDomainRequirement
            {
                SucursalExistente = sucursalExistente,
                TransportistaExistente = transportistaExistente
            };

        public bool SucursalExistente { get; set; }
        public bool TransportistaExistente { get; set; }

        public bool IsValid() => SucursalExistente && TransportistaExistente;

        public string ObtenerMensajesError()
        {
            var errors = new List<string>();
            if (!SucursalExistente)
                errors.Add("No se encontró la sucursal.");
            if (!TransportistaExistente)
                errors.Add("No se encontró el transportista.");
            return string.Join(" ", errors);
        }
    }
}
