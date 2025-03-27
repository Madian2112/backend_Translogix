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
        public TranslogixDBContext(DbContextOptions<TranslogixDBContext> options) : base(options)
        {
        }
        public DbSet<Cargos> Cargos => Set<Cargos>();
        public DbSet<Colaboradores> Colaboradores => Set<Colaboradores>();
        public DbSet<EstadosCiviles> EstadosCiviles => Set<EstadosCiviles>();
        public DbSet<Monedas> Monedas => Set<Monedas>();
        public DbSet<Paises> Paises => Set<Paises>();
        public DbSet<Personas> Personas => Set<Personas>();
        public DbSet<Roles> Roles => Set<Roles>();
        public DbSet<ServiciosTransporte> ServiciosTransporte => Set<ServiciosTransporte>();
        public DbSet<ServiciosTransporteTransportistas> ServiciosTransporteTransportistas => Set<ServiciosTransporteTransportistas>();
        public DbSet<Sucursales> Sucursales => Set<Sucursales>();
        public DbSet<SucursalesColaboradores> SucursalesColaboradores => Set<SucursalesColaboradores>();
        public DbSet<Tarifas> Tarifas => Set<Tarifas>();
        public DbSet<Transportistas> Transportistas => Set<Transportistas>();
        public DbSet<Usuarios> Usuarios => Set<Usuarios>();
        public DbSet<Viajes> Viajes => Set<Viajes>();
        public DbSet<ViajesDetalles> ViajesDetalles => Set<ViajesDetalles>();


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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
