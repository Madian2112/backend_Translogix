using Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase.Entities.Acce;
using Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase.Entities.Gral;
using Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase.Entities.Viaj;
using Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase.Maps.Acce;
using Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase.Maps.Gral;
using Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase.Maps.Viaj;
using Microsoft.EntityFrameworkCore;

namespace Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase
{
    public class TranslogixDBContext : DbContext
    {
        private readonly IHostEnvironment _environment;
        public TranslogixDBContext(DbContextOptions<TranslogixDBContext> options) : base(options)
        {
        }

        public DbSet<Cargos> Cargos => Set<Cargos>();
        public DbSet<Colaboradores> Colaboradores => Set<Colaboradores>();
        public DbSet<Estados_Civiles> EstadosCiviles => Set<Estados_Civiles>();
        public DbSet<Monedas> Monedas => Set<Monedas>();
        public DbSet<Paises> Paises => Set<Paises>();
        public DbSet<Personas> Personas => Set<Personas>();
        public DbSet<Roles> Roles => Set<Roles>();
        public DbSet<Servicios_Transporte> ServiciosTransporte => Set<Servicios_Transporte>();
        public DbSet<Servicios_Transporte_Transportistas> ServiciosTransporteTransportistas => Set<Servicios_Transporte_Transportistas>();
        public DbSet<Sucursales> Sucursales => Set<Sucursales>();
        public DbSet<Sucursales_Colaboradores> SucursalesColaboradores => Set<Sucursales_Colaboradores>();
        public DbSet<Tarifas> Tarifas => Set<Tarifas>();
        public DbSet<Transportistas> Transportistas => Set<Transportistas>();
        public DbSet<Usuarios> Usuarios => Set<Usuarios>();
        public DbSet<Viajes> Viajes => Set<Viajes>();
        public DbSet<Viajes_Detalles> ViajesDetalles => Set<Viajes_Detalles>();


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //if (_environment.IsEnvironment("test"))
            //{
            //    // Deshabilitar las claves foráneas en el entorno de pruebas
            //    foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            //    {
            //        relationship.DeleteBehavior = DeleteBehavior.ClientNoAction; // Ignorar acciones de eliminación
            //    }
            //}

            modelBuilder.ApplyConfiguration(new CargosMap());
            modelBuilder.ApplyConfiguration(new ColaboradoresMap());
            modelBuilder.ApplyConfiguration(new EstadosCivilesMap());
            modelBuilder.ApplyConfiguration(new MonedasMap());
            modelBuilder.ApplyConfiguration(new PaisesMap());
            modelBuilder.ApplyConfiguration(new PersonasMap());
            modelBuilder.ApplyConfiguration(new RolesMap());
            modelBuilder.ApplyConfiguration(new ServiciosTransporteMap());
            modelBuilder.ApplyConfiguration(new ServiciosTransporteTransportistasMap());
            modelBuilder.ApplyConfiguration(new SucursalesMap());
            modelBuilder.ApplyConfiguration(new SucursalesColaboradoresMap());
            modelBuilder.ApplyConfiguration(new TarifasMap());
            modelBuilder.ApplyConfiguration(new TransportistasMap());
            modelBuilder.ApplyConfiguration(new UsuariosMap());
            modelBuilder.ApplyConfiguration(new ViajesMap());
            modelBuilder.ApplyConfiguration(new ViajesDetallesMap());
        }
    }
}
