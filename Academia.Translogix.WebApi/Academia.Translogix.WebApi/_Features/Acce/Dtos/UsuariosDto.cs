using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Academia.Translogix.WebApi._Features.Acce.Dtos
{
    public class UsuariosDto
    {
        public int usuario_id { get; set; }
        public string nombre { get; set; } = string.Empty;
        [JsonIgnore]
        public byte[] clave { get; set; } = [];
        public bool es_admin { get; set; }
        public bool es_activo { get; set; }
        public int rol_id { get; set; }
        public int colaborador_id { get; set; }
    }

    public class UsuariosDtoInsertar
    {
        [Required]
        [MaxLength(150)]
        public string nombre { get; set; } = string.Empty;
        public string claveinput { get; set; } = string.Empty;
        [JsonIgnore]
        public byte[] clave { get; set; } = [];
        [Required]
        public bool es_admin { get; set; } = false;
        [Required]
        public bool es_activo { get; set; } = true;
        [Required]
        public int usuario_creacion { get; set; }
        [Required]
        public DateTime fecha_creacion { get; set; } = DateTime.Now;
        [Required]
        public int? rol_id { get; set; }
        [Required]
        public int? colaborador_id { get; set; }
    }

    public class UsuariosDtoActualizar
    {
        [Required]
        [MaxLength(150)]
        public string nombre { get; set; } = string.Empty;
        [Required]
        public bool es_admin { get; set; } = false;
        [Required]
        public int usuario_modificacion { get; set; }
        [Required]
        public DateTime fecha_modificacion { get; set; } = DateTime.Now;
        [Required]
        public int rol_id { get; set; }
        [Required]
        public int colaborador_id { get; set; }
    }
}
