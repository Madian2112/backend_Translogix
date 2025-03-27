using System.Text.Json.Serialization;
using Academia.Translogix.WebApi._Features.Gral.Dtos;

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
        public string rol { get; set; } = string.Empty;
        public string cargo { get; set; } = string.Empty;
        public ColaboradoresDto Colaborador { get; set; } = new ColaboradoresDto();
        public PersonasDto Persona { get; set; } = new PersonasDto();

    }

    public class UsuariosDtoInsertar
    {
        public string nombre { get; set; } = string.Empty;
        public string claveinput { get; set; } = string.Empty;
        [JsonIgnore]
        public byte[] clave { get; set; } = [];
        public bool es_admin { get; set; } = false;
        public bool es_activo { get; set; } = true;
        public int usuario_creacion { get; set; }
        public DateTime fecha_creacion { get; set; } = DateTime.Now;
        public int rol_id { get; set; }
        public int colaborador_id { get; set; }
    }

    public class UsuariosDtoActualizar
    {
        public string nombre { get; set; } = string.Empty;
        public bool es_admin { get; set; } = false;
        public int usuario_modificacion { get; set; }
        public DateTime fecha_modificacion { get; set; } = DateTime.Now;
        public int rol_id { get; set; }
        public int colaborador_id { get; set; }
    }

    public class UsuarioInicioSesion
    {
        public string usuario { get; set; } = string.Empty;
        public string clave { get; set; } = string.Empty;
    }
}
