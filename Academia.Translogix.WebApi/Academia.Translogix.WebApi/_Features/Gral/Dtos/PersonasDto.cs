using System.Text.Json.Serialization;

namespace Academia.Translogix.WebApi._Features.Gral.Dtos
{
    public class PersonasDto
    {
        public int persona_id { get; set; }
        public string identidad { get; set; } = string.Empty;
        public string primer_nombre { get; set; } = string.Empty;
        public string segundo_nombre { get; set; } = string.Empty;
        public string primer_apellido { get; set; } = string.Empty;
        public string segundo_apellido { get; set; } = string.Empty;
        public char sexo { get; set; }
        public string telefono { get; set; } = string.Empty;
        public string correo_electronico { get; set; } = string.Empty;
        public int pais_id { get; set; }
        public bool es_activo { get; set; }


    }
    public class PersonasDtoInsertar
    {
        public string identidad { get; set; } = string.Empty;
        public string primer_nombre { get; set; } = string.Empty;
        public string segundo_nombre { get; set; } = string.Empty;
        public string primer_apellido { get; set; } = string.Empty;
        public string segundo_apellido { get; set; } = string.Empty;
        public char sexo { get; set; }
        public string telefono { get; set; } = string.Empty;
        public string correo_electronico { get; set; } = string.Empty;
        public int pais_id { get; set; }
        public bool es_activo { get; set; } = true;
        [JsonIgnore]
        public int usuario_creacion { get; set; }
        [JsonIgnore]
        public DateTime fecha_creacion { get; set; } = DateTime.Now;
    }
}
