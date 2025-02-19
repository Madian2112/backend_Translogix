﻿namespace Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase.Entities
{
    public class Colaboradores
    {
        public int colaborador_id { get; set; }
        public string fecha_nacimiento { get; set; } = string.Empty;
        public decimal latitud { get; set; }
        public decimal longitud { get; set; }
        public int estado_civil_id { get; set; }
        public int cargo_id { get; set; }
        public int persona_id { get; set; }
        public int usuario_creacion { get; set; }
        public DateTime fecha_creacion { get; set; }
        public int? usuario_modificacion { get; set; }
        public DateTime? fecha_modificacion { get; set; }
        public bool es_activo { get; set; }

        public Estados_Civiles Estados_Civiles { get; set; } = null!;
        public Cargos Cargos { get; set; } = null!;
        public Personas Personas { get; set; } = null!;
        public Usuarios UsuarioCrear { get; set; } = null!;
        public Usuarios? UsuarioModificar { get; set; }

        public ICollection<Usuarios> Usuarios { get; set; } = new HashSet<Usuarios>();

    }
}
