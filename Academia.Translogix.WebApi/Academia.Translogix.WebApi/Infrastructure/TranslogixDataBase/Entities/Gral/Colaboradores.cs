using Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase.Entities.Acce;
using Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase.Entities.Viaj;

namespace Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase.Entities.Gral
{
    public class Colaboradores
    {
        public int colaborador_id { get; set; }
        public DateTime fecha_nacimiento { get; set; }
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

        public Estados_Civiles EstadoCivil { get; set; } = null!;
        public Cargos Cargo { get; set; } = null!;
        public Personas Persona { get; set; } = null!;
        public Usuarios UsuarioCrear { get; set; } = null!;
        public Usuarios? UsuarioModificar { get; set; }

        public ICollection<Usuarios> Usuarios { get; set; } = new List<Usuarios>();
        public ICollection<Sucursales_Colaboradores> SucursalesColaboradores { get; set; } = new List<Sucursales_Colaboradores>();
        public ICollection<Viajes_Detalles> ViajesDetalles { get; set; } = new List<Viajes_Detalles>();


    }
}
