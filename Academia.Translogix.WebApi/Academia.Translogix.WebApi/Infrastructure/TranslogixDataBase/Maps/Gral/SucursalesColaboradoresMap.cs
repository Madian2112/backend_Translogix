using Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase.Entities.Viaj;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase.Maps.Gral
{
    public class SucursalesColaboradoresMap : IEntityTypeConfiguration<SucursalesColaboradores>
    {
        public void Configure(EntityTypeBuilder<SucursalesColaboradores> builder)
        {
            builder.ToTable("Sucursales_Colaboradores");
            builder.HasKey(x => x.sucursal_empleado_id);
            builder.Property(x => x.distancia_empleado_sucursal_km).HasColumnType("decimal(10,2)").IsRequired();
            builder.Property(x => x.colaborador_id).IsRequired();
            builder.Property(x => x.sucursal_id).IsRequired();

            builder.Property(x => x.usuario_creacion).IsRequired();
            builder.Property(x => x.fecha_creacion).IsRequired();
            builder.Property(x => x.usuario_modificacion).IsRequired(false);
            builder.Property(x => x.fecha_modificacion).IsRequired(false);
            builder.Property(x => x.es_activo).IsRequired();

            builder.HasOne(x => x.Colaborador)
                .WithMany(x => x.SucursalesColaboradores)
                .HasForeignKey(x => x.colaborador_id);

            builder.HasOne(x => x.Sucursal)
                .WithMany(x => x.SucursalesColaboradores)
                .HasForeignKey(x => x.sucursal_id);

            builder.HasOne(x => x.UsuarioCrear)
                .WithMany(x => x.SucursalesColaboradoresCreacion)
                .HasForeignKey(x => x.usuario_creacion);

            builder.HasOne(x => x.UsuarioModificar)
                .WithMany(x => x.SucursalesColaboradoresModifiacion)
                .HasForeignKey(x => x.usuario_modificacion);
        }
    }
}
