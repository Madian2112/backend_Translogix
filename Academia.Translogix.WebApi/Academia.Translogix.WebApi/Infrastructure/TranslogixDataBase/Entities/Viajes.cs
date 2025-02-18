namespace Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase.Entities
{
    public class Viajes
    {
        public int viaje_id { get; set; }
        public DateTime fecha { get; set; }
        public decimal distancia_recorrida_km { get; set; }
        public decimal total_pagar { get; set; }
        public int sucursal_id { get; set; }
        public int usuario_id { get; set; }
        public int transportista_id { get; set; }
        public int usuario_creacion { get; set; }
        public DateTime fecha_creacion { get; set; }
        public int? usuario_modificacion { get; set; }
        public DateTime? fecha_modificacion { get; set; }
        public bool es_activo { get; set; }

        public Sucursales Sucursal { get; set; }
        public Usuarios Usuario { get; set; }
        public Transportistas Transportista { get; set; }
        public ICollection<Viajes_Detalles> ViajesDetalles { get; set; }

    }
}
