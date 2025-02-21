using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Academia.Translogix.WebApi._Features.Viaj.Dtos
{
    public class SucursalesDto
    {
        public int sucursal_id { get; set; }
        public string nombre { get; set; } = string.Empty;
        public bool es_activo { get; set; }
        public decimal latitud { get; set; }
        public decimal longitud { get; set; }

    }

    public class SucursalesDtoInsertar
    {
        [Required]
        [MaxLength(200)]
        public string nombre { get; set; } = string.Empty;
        [Required]
        public decimal latitud { get; set; }
        [Required]
        public decimal longitud { get; set; }
        [Required]
        public int usuario_creacion { get; set; }
        [Required]
        public DateTime fecha_creacion { get; set; } = DateTime.Now;
        [Required]
        public bool es_activo { get; set; }
    }

    public class SucursalesDtoActualizar
    {        
        [Required]
        [MaxLength(200)]
        public string nombre { get; set; } = string.Empty;
        [Required]
        public decimal latitud { get; set; }
        [Required]
        public decimal longitud { get; set; }
        [Required]
        public int usuario_modificacion { get; set; }
        [Required]
        public DateTime fecha_modificacion { get; set; } = DateTime.Now;
        [Required]
        public bool es_activo { get; set; }
    }
}