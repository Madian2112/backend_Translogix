namespace Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase.Entities
{
    public class Monedas
    {
        public int moneda_id { get; set; }
        public string nombre { get; set; }
        public string abreviatura { get; set; }
        public decimal valor_lempira { get; set; }
        public int? pais_id { get; set; }
        public int usuario_creacion { get; set; }
        public DateTime fecha_creacion { get; set; }
        public int? usuario_modificacion { get; set; }
        public DateTime? fecha_modificacion { get; set; }
        public bool? es_activo { get; set; }

        public Paises Paises { get; set; }
        public ICollection<Transportistas> Transportistas { get; set; }

    }
}
