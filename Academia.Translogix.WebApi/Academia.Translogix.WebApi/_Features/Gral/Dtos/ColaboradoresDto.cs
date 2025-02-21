using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Academia.Translogix.WebApi._Features.Gral.Dtos
{
    public class ColaboradoresDto
    {
        public int colaborador_id { get; set; }
        public DateTime fecha_nacimiento { get; set; }
        public decimal latitud { get; set; }
        public decimal longitud { get; set; }
        public int estado_civil_id { get; set; }
        public int cargo_id { get; set; }
        public int persona_id { get; set; }
        public bool es_activo { get; set; }
    }

    public class ColaboradoresDtoInsertar
    {
        [Required]
        public DateTime fecha_nacimiento { get; set; } = DateTime.Now;
        [Required]
        public decimal latitud { get; set; }
        [Required]
        public decimal longitud { get; set; }
        [Required]
        public int estado_civil_id { get; set; }
        [Required]
        public int cargo_id { get; set; }
        [Required]
        public int persona_id { get; set; }
        [Required]
        public int usuario_creacion { get; set; }
        [Required]
        public DateTime fecha_creacion { get; set; } = DateTime.Now;
        [Required]
        public bool es_activo { get; set; }
    }

    public class ColaboradoresDtoActualizar
    {
        [Required]
        public DateTime fecha_nacimiento { get; set; } = DateTime.Now;
        [Required]
        public decimal latitud { get; set; }
        [Required]
        public decimal longitud { get; set; }
        [Required]
        public int estado_civil_id { get; set; }
        [Required]
        public int cargo_id { get; set; }
        [Required]
        public int persona_id { get; set; }
        [Required]
        public int usuario_modificacion { get; set; }
        [Required]
        public DateTime fecha_modificacion { get; set; } = DateTime.Now;
    }
}