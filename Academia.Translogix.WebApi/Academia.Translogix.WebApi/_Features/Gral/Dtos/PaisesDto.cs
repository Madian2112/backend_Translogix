namespace Academia.Translogix.WebApi._Features.Gral.Dtos
{
    public class PaisesDto
    {
        public int pais_id { get; set; }
        public string prefijo { get; set; } = string.Empty;
        public string nombre { get; set; } = string.Empty;
        public bool? es_activo { get; set; } = true;
    }
}
