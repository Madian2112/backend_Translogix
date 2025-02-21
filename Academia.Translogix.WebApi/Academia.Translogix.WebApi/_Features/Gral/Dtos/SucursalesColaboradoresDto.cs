using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Academia.Translogix.WebApi._Features.Gral.Dtos
{
    public class SucursalesColaboradoresDto
    {
        public int sucursal_empleado_id { get; set; }
        public decimal distancia_empleado_sucursal_km { get; set; }
        public int colaborador_id { get; set; }
        public int sucursal_id { get; set; }
        public bool es_activo { get; set; }
    }

    public class SucursalesColaboradoresInsertarDto
    {
        [JsonIgnore]
        public decimal distancia_empleado_sucursal_km { get; set; }
        [Required]
        public int colaborador_id { get; set; }
        [Required]
        public int sucursal_id { get; set; }
        [Required]
        public int usuario_creacion { get; set; }
        [Required]
        public DateTime fecha_creacion { get; set; } = DateTime.Now;
        [Required]
        public bool es_activo { get; set; }
    }

    public class SucursalesColaboradoresActualizarDto
    {
        [JsonIgnore]
        public decimal distancia_empleado_sucursal_km { get; set; }
        [Required]
        public int colaborador_id { get; set; }
        [Required]
        public int sucursal_id { get; set; }
        [Required]
        public int usuario_modificacion { get; set; }
        [Required]
        public DateTime fecha_modificacion { get; set; } = DateTime.Now;
        [Required]
        public bool es_activo { get; set; }
    }
}