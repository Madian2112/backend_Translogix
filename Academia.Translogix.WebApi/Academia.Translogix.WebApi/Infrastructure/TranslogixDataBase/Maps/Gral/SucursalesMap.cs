using Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase.Entities.Viaj;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase.Maps.Gral
{
    public class SucursalesMap : IEntityTypeConfiguration<Sucursales>
    {
        public void Configure(EntityTypeBuilder<Sucursales> builder)
        {
            builder.ToTable("Sucursales");
            builder.HasKey(x => x.sucursal_id);
            builder.Property(x => x.nombre).HasMaxLength(200).IsRequired();
            builder.Property(x => x.nombre).HasMaxLength(100).IsRequired();
            builder.Property(x => x.latitud).HasColumnType("decimal(19,15)").IsRequired();
            builder.Property(x => x.longitud).HasColumnType("decimal(19,15)").IsRequired();

            builder.Property(x => x.usuario_creacion).IsRequired();
            builder.Property(x => x.fecha_creacion).IsRequired();
            builder.Property(x => x.usuario_modificacion).IsRequired(false);
            builder.Property(x => x.fecha_modificacion).IsRequired(false);
            builder.Property(x => x.es_activo).IsRequired();

            builder.HasOne(x => x.UsuarioCrear)
                .WithMany(x => x.SucursalesCreacion)
                .HasForeignKey(x => x.usuario_creacion);

            builder.HasOne(x => x.UsuarioModificar)
                .WithMany(x => x.SucursalesModifiacion)
                .HasForeignKey(x => x.usuario_modificacion);
        }
    }
}
