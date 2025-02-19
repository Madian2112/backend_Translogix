namespace Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase.Entities
{
    public class Servicios_Transporte_Transportistas
    {
        public int servicio_transporte_transportista_id { get; set; }
        public int? servicio_transporte_id { get; set; }
        public int transportista_id { get; set; }
        public int usuario_creacion { get; set; }
        public DateTime fecha_creacion { get; set; }
        public int? usuario_modificacion { get; set; }
        public DateTime? fecha_modificacion { get; set; }
        public bool es_activo { get; set; }
        
        public Usuarios UsuarioCrear { get; set; }
        public Usuarios UsuarioModificar { get; set; }
    }
}
