namespace Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase.Entities
{
    public class Viajes_Detalles
    {
        public int viaje_detalle_id { get; set; }
        public decimal total_pagar_por_km { get; set; }
        public int viaje_id { get; set; }
        public int colaborador_id { get; set; }
        public int usuario_creacion { get; set; }
        public DateTime fecha_creacion { get; set; }
        public int? usuario_modificacion { get; set; }
        public DateTime? fecha_modificacion { get; set; }
        public bool es_activo { get; set; }

        public Viajes Viaje { get; set; }
        public Colaboradores Colaborador { get; set; }
        public Usuarios UsuarioCrear { get; set; }
        public Usuarios UsuarioModificar { get; set; }
    }
}
