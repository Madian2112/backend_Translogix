using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase.Entities.Acce;
using Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase.Entities.Gral;

namespace Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase.Entities.Viaj
{
    [ExcludeFromCodeCoverage]
    public class Transportistas
    {
        [Key]
        public int transportista_id { get; set; }
        public int moneda_id { get; set; }
        public int persona_id { get; set; }
        public int tarifa_id { get; set; }
        public int usuario_creacion { get; set; }
        public DateTime fecha_creacion { get; set; }
        public int? usuario_modificacion { get; set; }
        public DateTime? fecha_modificacion { get; set; }
        public bool es_activo { get; set; }


        public Personas Persona { get; set; } = null!;
        public Tarifas Tarifa { get; set; } = null!;
        public Monedas Moneda { get; set; } = null!;
        public Usuarios UsuarioCrear { get; set; } = null!;
        public Usuarios? UsuarioModificar { get; set; }
        public ICollection<Servicios_Transporte_Transportistas> ServiciosTransporteTransportistas { get; set; } = new List<Servicios_Transporte_Transportistas>();
        public ICollection<Viajes> Viajes { get; set; } = new List<Viajes>();

    }
}
