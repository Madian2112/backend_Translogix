namespace Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase.Entities
{
    public class Roles
    {
        public int rol_id { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public int usuario_creacion { get; set; }
        public DateTime fecha_creacion { get; set; }
        public int? usuario_modificacion { get; set; }
        public DateTime? fecha_modificacion { get; set; }
        public bool es_activo { get; set; }

        public Usuarios UsuarioCrear { get; set; }
        public Usuarios UsuarioModificar { get; set; }
        public ICollection<Usuarios> Usuarios { get; set; }
    }
}
