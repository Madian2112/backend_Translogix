namespace Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase.Entities
{
    public class Sucursales
    {
        public int sucursal_id { get; set; }
        public string nombre { get; set; }
        public int usuario_creacion { get; set; }
        public DateTime fecha_creacion { get; set; }
        public int? usuario_modificacion { get; set; }
        public DateTime? fecha_modificacion { get; set; }
        public bool es_activo { get; set; }
        public decimal latitud { get; set; }
        public decimal longitud { get; set; }

        public ICollection<Sucursales_Colaboradores> SucursalesColaboradores { get; set; }
    }
}
