namespace Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase.Entities
{
    public class Sucursales_Colaboradores
    {
        public int sucursal_empleado_id { get; set; }
        public decimal distancia_empleado_sucursal_km { get; set; }
        public int colaborador_id { get; set; }
        public int sucursal_id { get; set; }
        public int usuario_creacion { get; set; }
        public DateTime fecha_creacion { get; set; }
        public int? usuario_modificacion { get; set; }
        public DateTime? fecha_modificacion { get; set; }
        public bool es_activo { get; set; }

        public Colaboradores Colaborador { get; set; }
        public Sucursales Sucursal { get; set; }
        public Usuarios UsuarioCrear { get; set; }
        public Usuarios UsuarioModificar { get; set; }

        
    }
}
