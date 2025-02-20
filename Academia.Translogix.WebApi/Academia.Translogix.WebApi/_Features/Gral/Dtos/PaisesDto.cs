using System.ComponentModel.DataAnnotations;

namespace Academia.Translogix.WebApi._Features.Gral.Dtos
{
    public class PaisesDto
    {
        public int pais_id { get; set; }
        public string prefijo { get; set; } = string.Empty;
        public string nombre { get; set; } = string.Empty;
        public bool es_activo { get; set; } = true;
    }

    public class PaisesDtoInsertar
    {
        public int pais_id { get; set; }
        [Required]
        public int prefijo { get; set; }
        [Required]
        [MaxLength(100)]
        public string nombre { get; set; } = string.Empty;
        [Required]
        public int usuario_creacion { get; set; }
        [Required]
        public DateTime fecha_creacion { get; set; } = DateTime.Now;
        [Required]
        public bool es_activo { get; set; } = true;
    }

    public class PaisesDtoActualizar
    {
        [Required]
        public string prefijo { get; set; } = string.Empty;
        [Required]
        [MaxLength(100)]
        public string nombre { get; set; } = string.Empty;
        public int usuario_modificacion { get; set; }
        [Required]
        public DateTime fecha_modificacion { get; set; } = DateTime.Now;
    }
}
