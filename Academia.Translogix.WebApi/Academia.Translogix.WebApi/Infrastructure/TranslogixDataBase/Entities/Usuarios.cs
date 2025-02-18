namespace Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase.Entities
{
    public class Usuarios
    {
        public int usuario_id { get; set; }
        public string nombre { get; set; }
        public string clave { get; set; }
        public bool es_admin { get; set; }
        public bool es_activo { get; set; }
        public int? usuario_creacion { get; set; }
        public DateTime? fecha_creacion { get; set; }
        public int? usuario_modificacion { get; set; }
        public DateTime? fecha_modificacion { get; set; }
        public int? rol_id { get; set; }
        public int? colaborador_id { get; set; }

        public ICollection<Colaboradores> Colaboradores { get; set; }

    }
}
