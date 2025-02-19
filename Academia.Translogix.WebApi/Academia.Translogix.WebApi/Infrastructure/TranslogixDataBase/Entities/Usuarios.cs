namespace Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase.Entities
{
    public class Usuarios
    {
        public int usuario_id { get; set; }
        public string nombre { get; set; } = string.Empty;
        public string clave { get; set; } = string.Empty;
        public bool es_admin { get; set; }
        public bool es_activo { get; set; }
        public int? usuario_creacion { get; set; }
        public DateTime? fecha_creacion { get; set; }
        public int? usuario_modificacion { get; set; }
        public DateTime? fecha_modificacion { get; set; }
        public int? rol_id { get; set; }
        public int? colaborador_id { get; set; }

        public ICollection<Cargos> CargosCreacion { get; set; } = new HashSet<Cargos>();
        public ICollection<Cargos> CargosModificacion { get; set; } = new HashSet<Cargos>();
        public ICollection<Colaboradores> ColaboradoresCreacion { get; set; } = new HashSet<Colaboradores>();
        public ICollection<Colaboradores> ColaboradoresModificacion { get; set; } = new HashSet<Colaboradores>();
        public ICollection<Estados_Civiles> EstadosCivilesCreacion { get; set; } = new HashSet<Estados_Civiles>();
        public ICollection<Estados_Civiles> EstadosCivilesModificacion { get; set; } = new HashSet<Estados_Civiles>();
        public ICollection<Monedas> MonedasCreacion { get; set; } = new HashSet<Monedas>(); 
        public ICollection<Monedas> MonedasModificacion { get; set; } = new HashSet<Monedas>(); 
        public ICollection<Paises> PaisesCreacion { get; set; } = new HashSet<Paises>(); 
        public ICollection<Paises> PaisesModifiacion { get; set; } = new HashSet<Paises>(); 

    }
}
