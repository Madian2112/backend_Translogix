using Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase.Entities.Gral;
using Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase.Entities.Viaj;

namespace Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase.Entities.Acce
{
    public class Usuarios
    {
        public int usuario_id { get; set; }
        public string nombre { get; set; } = string.Empty;
        public byte[] clave { get; set; } = null!;
        public bool es_admin { get; set; }
        public bool es_activo { get; set; }
        public int? usuario_creacion { get; set; }
        public DateTime? fecha_creacion { get; set; }
        public int? usuario_modificacion { get; set; }
        public DateTime? fecha_modificacion { get; set; }
        public int? rol_id { get; set; }
        public int? colaborador_id { get; set; }

        public Roles Rol { get; set; } = null!;
        public Colaboradores Colaborador { get; set; } = null!;
        public Usuarios? UsuarioCrear { get; set; } = null!;
        public Usuarios? UsuarioModificar { get; set; }

        public ICollection<Cargos> CargosCreacion { get; set; } = new List<Cargos>();
        public ICollection<Cargos> CargosModificacion { get; set; } = new List<Cargos>();
        public ICollection<Colaboradores> ColaboradoresCreacion { get; set; } = new List<Colaboradores>();
        public ICollection<Colaboradores> ColaboradoresModificacion { get; set; } = new List<Colaboradores>();
        public ICollection<Estados_Civiles> EstadosCivilesCreacion { get; set; } = new List<Estados_Civiles>();
        public ICollection<Estados_Civiles> EstadosCivilesModificacion { get; set; } = new List<Estados_Civiles>();
        public ICollection<Monedas> MonedasCreacion { get; set; } = new List<Monedas>();
        public ICollection<Monedas> MonedasModificacion { get; set; } = new List<Monedas>();
        public ICollection<Paises> PaisesCreacion { get; set; } = new List<Paises>();
        public ICollection<Paises> PaisesModifiacion { get; set; } = new List<Paises>();
        public ICollection<Personas> PersonasCreacion { get; set; } = new List<Personas>();
        public ICollection<Personas> PersonasModifiacion { get; set; } = new List<Personas>();
        public ICollection<Roles> RolesCreacion { get; set; } = new List<Roles>();
        public ICollection<Roles> RolesModifiacion { get; set; } = new List<Roles>();
        public ICollection<Servicios_Transporte> ServiciosTransporteCreacion { get; set; } = new List<Servicios_Transporte>();
        public ICollection<Servicios_Transporte> ServiciosTransporteModifiacion { get; set; } = new List<Servicios_Transporte>();
        public ICollection<Servicios_Transporte_Transportistas> ServiciosTransporteTransportistasCreacion { get; set; } = new List<Servicios_Transporte_Transportistas>();
        public ICollection<Servicios_Transporte_Transportistas> ServiciosTransporteTransportistasModifiacion { get; set; } = new List<Servicios_Transporte_Transportistas>();
        public ICollection<Sucursales> SucursalesCreacion { get; set; } = new List<Sucursales>();
        public ICollection<Sucursales> SucursalesModifiacion { get; set; } = new List<Sucursales>();
        public ICollection<Sucursales_Colaboradores> SucursalesColaboradoresCreacion { get; set; } = new List<Sucursales_Colaboradores>();
        public ICollection<Sucursales_Colaboradores> SucursalesColaboradoresModifiacion { get; set; } = new List<Sucursales_Colaboradores>();
        public ICollection<Tarifas> TarifasCreacion { get; set; } = new List<Tarifas>();
        public ICollection<Tarifas> TarifasModifiacion { get; set; } = new List<Tarifas>();
        public ICollection<Transportistas> TransportistasCreacion { get; set; } = new List<Transportistas>();
        public ICollection<Transportistas> TransportistasModifiacion { get; set; } = new List<Transportistas>();
        public ICollection<Usuarios> UsuariosCreacion { get; set; } = new List<Usuarios>();
        public ICollection<Usuarios> UsuariosModifiacion { get; set; } = new List<Usuarios>();
        public ICollection<Viajes> ViajesEncabezado { get; set; } = new List<Viajes>();
        public ICollection<Viajes> ViajesCreacion { get; set; } = new List<Viajes>();
        public ICollection<Viajes> ViajesModifiacion { get; set; } = new List<Viajes>();
        public ICollection<Viajes_Detalles> ViajesDetallesCreacion { get; set; } = new List<Viajes_Detalles>();
        public ICollection<Viajes_Detalles> ViajesDetallesModifiacion { get; set; } = new List<Viajes_Detalles>();

    }
}
