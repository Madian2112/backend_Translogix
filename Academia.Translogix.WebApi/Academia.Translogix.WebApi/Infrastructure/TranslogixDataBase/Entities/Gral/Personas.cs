using System.ComponentModel.DataAnnotations;
using Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase.Entities.Acce;
using Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase.Entities.Viaj;

namespace Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase.Entities.Gral
{
    public class Personas
    {
        [Key]
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
        public int? usuario_creacion { get; set; }
        public DateTime? fecha_creacion { get; set; }
        public int? usuario_modificacion { get; set; }
        public DateTime? fecha_modificacion { get; set; }
        public bool es_activo { get; set; }

        public Paises Pais { get; set; } = null!;
        public Usuarios UsuarioCrear { get; set; } = null!;
        public Usuarios? UsuarioModificar { get; set; }
        public ICollection<Colaboradores> Colaboradores { get; set; } = new List<Colaboradores>();
        public ICollection<Transportistas> Transportistas { get; set; } = new List<Transportistas>();
    }
}
