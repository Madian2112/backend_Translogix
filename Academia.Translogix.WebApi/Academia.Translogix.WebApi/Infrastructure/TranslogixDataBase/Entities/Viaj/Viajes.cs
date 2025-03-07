using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase.Entities.Acce;
using Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase.Entities.Gral;

namespace Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase.Entities.Viaj
{
    [ExcludeFromCodeCoverage]
    public class Viajes
    {
        [Key]
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

        public Sucursales Sucursal { get; set; } = null!;
        public Usuarios Usuario { get; set; } = null!;
        public Transportistas Transportista { get; set; } = null!;
        public Usuarios UsuarioCrear { get; set; } = null!;
        public Usuarios? UsuarioModificar { get; set; }

        public ICollection<Viajes_Detalles> ViajesDetalles { get; set; } = new List<Viajes_Detalles>();


    }
}
