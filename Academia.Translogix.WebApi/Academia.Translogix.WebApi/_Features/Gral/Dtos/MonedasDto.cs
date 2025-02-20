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

    public class MonedasDtoActualizar
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
}
