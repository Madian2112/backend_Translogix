namespace Academia.Translogix.WebApi._Features.Viaj.Dtos
{
    public class ViajesDetallesDto
    {
        public class ViajesDetalleInsertarDto
        {
            public decimal total_pagar_por_km { get; set; }
            public int viaje_id { get; set; }
            public int colaborador_id { get; set; }
            public int usuario_creacion { get; set; }
            public decimal distancia_empleado_sucursal_km { get; set; }
            public DateTime fecha_creacion { get; set; }
            public bool es_activo { get; set; }
        }
    }
}
