using System.ComponentModel.DataAnnotations;
using Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase.Entities.Acce;
using Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase.Entities.Viaj;

namespace Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase.Entities.Gral
{
    public class Servicios_Transporte_Transportistas
    {
        [Key]
        public int servicio_transporte_transportista_id { get; set; }
        public int servicio_transporte_id { get; set; }
        public int transportista_id { get; set; }
        public int usuario_creacion { get; set; }
        public DateTime fecha_creacion { get; set; }
        public int? usuario_modificacion { get; set; }
        public DateTime? fecha_modificacion { get; set; }
        public bool es_activo { get; set; }

        public Servicios_Transporte ServiciosTransporte { get; set; } = null!;
        public Transportistas Transportista { get; set; } = null!;
        public Usuarios UsuarioCrear { get; set; } = null!;
        public Usuarios? UsuarioModificar { get; set; }
    }
}
