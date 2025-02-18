namespace Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase.Entities
{
    public class Transportistas
    {
        public int transportista_id { get; set; }
        public bool es_servicio_transporte { get; set; }
        public int? moneda_id { get; set; }
        public int persona_id { get; set; }
        public int tarifa_id { get; set; }
        public int usuario_creacion { get; set; }
        public DateTime fecha_creacion { get; set; }
        public int? usuario_modificacion { get; set; }
        public DateTime? fecha_modificacion { get; set; }
        public bool es_activo { get; set; }
    }
}
