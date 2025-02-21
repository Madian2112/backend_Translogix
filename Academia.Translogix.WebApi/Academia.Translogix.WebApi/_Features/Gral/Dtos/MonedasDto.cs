using System.ComponentModel.DataAnnotations;
using Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase.Entities;

namespace Academia.Translogix.WebApi._Features.Gral.Dtos
{
    public class MonedasDto
    {
        public int moneda_id { get; set; }

        [Required]
        [MaxLength(10)]
        public string nombre { get; set; } = string.Empty;

        [Required]
        public string abreviatura { get; set; } = string.Empty;
        public decimal valor_lempira { get; set; }
        public int pais_id { get; set; }
        public string paisNombre { get; set; } = string.Empty;
        public bool es_activo { get; set; } = true;

    }
    public class MonedasDtoInsertar
    {
        [Required]
        [MaxLength(100)]
        public string nombre { get; set; } = string.Empty;
        [Required]
        [MaxLength(10)]
        public string abreviatura { get; set; } = string.Empty;
        [Required]
        public decimal valor_lempira { get; set; }
        [Required]
        public int pais_id { get; set; }
        [Required]
        public int usuario_creacion { get; set; }
        [Required]
        public DateTime fecha_creacion { get; set; } = DateTime.Now;
        [Required]
        public bool es_activo { get; set; } = true;
    }

    public class MonedasDtoActualizar
    {
        [Required]
        [MaxLength(100)]
        public string nombre { get; set; } = string.Empty;
        [Required]
        [MaxLength(10)]
        public string abreviatura { get; set; } = string.Empty;
        [Required]
        public decimal valor_lempira { get; set; }
        [Required]
        public int pais_id { get; set; }
        [Required]
        public int usuario_modificacion { get; set; }
        [Required]
        public DateTime fecha_modificacion { get; set; } = DateTime.Now;
    }
}
