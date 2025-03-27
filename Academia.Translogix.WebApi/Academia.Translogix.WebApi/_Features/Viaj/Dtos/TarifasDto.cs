namespace Academia.Translogix.WebApi._Features.Viaj.Dtos
{
    public class TarifasDto
    {
        public int tarifa_id { get; set; }
        public decimal precio_km { get; set; }
        public bool es_activo { get; set; }
    }

    public class TarifasDtoInsertar
    {
        public decimal precio_km { get; set; }
        public int usuario_creacion { get; set; }
        public DateTime fecha_creacion { get; set; }
        public bool es_activo { get; set; }
    }

    public class TarifasDtoActualizar
    {
        public int tarifa_id { get; set; }
        public decimal precio_km { get; set; }
        public int? usuario_modificacion { get; set; }
        public DateTime? fecha_modificacion { get; set; }
    }
}