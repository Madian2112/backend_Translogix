namespace Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase.Entities
{
    public class Paises
    {
        public int pais_id { get; set; }
        public string prefijo { get; set; }
        public string nombre { get; set; }
        public int usuario_creacion { get; set; }
        public DateTime fecha_creacion { get; set; }
        public int? usuario_modificacion { get; set; }
        public DateTime? fecha_modificacion { get; set; }
        public bool? es_activo { get; set; }

        public ICollection<Monedas> Monedas { get; set; }

    }
}
