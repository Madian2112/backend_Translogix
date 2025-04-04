﻿using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase.Entities.Acce;

namespace Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase.Entities.Gral
{
    [ExcludeFromCodeCoverage]
    public class Servicios_Transporte
    {
        [Key]
        public int servicio_transporte_id { get; set; }
        public string nombre { get; set; } = string.Empty;
        public string telefono { get; set; } = string.Empty;
        public string correo_electronico { get; set; } = string.Empty;
        public int usuario_creacion { get; set; }
        public DateTime fecha_creacion { get; set; }
        public int? usuario_modificacion { get; set; }
        public DateTime? fecha_modificacion { get; set; }
        public bool es_activo { get; set; }

        public Usuarios UsuarioCrear { get; set; } = null!;
        public Usuarios? UsuarioModificar { get; set; }
        public ICollection<Servicios_Transporte_Transportistas> ServiciosTransporteTransportistas { get; set; } = new List<Servicios_Transporte_Transportistas>();


    }
}
