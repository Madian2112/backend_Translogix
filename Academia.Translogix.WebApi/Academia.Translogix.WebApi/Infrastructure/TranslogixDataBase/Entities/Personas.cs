namespace Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase.Entities
{
    public class Personas
    {
        public int persona_id { get; set; }
        public string identidad { get; set; }
        public string primer_nombre { get; set; }
        public string segundo_nombre { get; set; }
        public string primer_apellido { get; set; }
        public string segundo_apellido { get; set; }
        public char sexo { get; set; }
        public string telefono { get; set; }
        public string correo_electronico { get; set; }
        public int usuario_creacion { get; set; }
        public DateTime fecha_creacion { get; set; }
        public int? usuario_modificacion { get; set; }
        public DateTime? fecha_modificacion { get; set; }
        public bool es_activo { get; set; }

        public ICollection<Colaboradores> Colaboradores { get; set; }
        public ICollection<Transportistas> Transportistas { get; set; }
    }
}
