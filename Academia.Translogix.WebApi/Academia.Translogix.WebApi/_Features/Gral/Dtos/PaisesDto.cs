using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

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
        [Required]
        public int prefijo { get; set; }
        [Required]
        [MaxLength(100)]
        public string nombre { get; set; } = string.Empty;
        [JsonIgnore]
        public int usuario_creacion { get; set; } = 3;
        [JsonIgnore]
        public DateTime fecha_creacion { get; set; } = DateTime.Now;
        [JsonIgnore]
        public bool es_activo { get; set; } = true;
    }

    public class PaisesDtoActualizar
    {
        [Required]
        public string prefijo { get; set; } = string.Empty;
        [Required]
        [MaxLength(100)]
        public string nombre { get; set; } = string.Empty;
        [JsonIgnore]
        public int usuario_modificacion { get; set; } = 3;
        [JsonIgnore]
        public DateTime fecha_modificacion { get; set; } = DateTime.Now;
    }
}
