using System.Diagnostics.CodeAnalysis;
using Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase.Entities.Viaj;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase.Maps.Viaj
{
    public class ViajesMap : IEntityTypeConfiguration<Viajes>
    {
        public void Configure(EntityTypeBuilder<Viajes> builder)
        {
            builder.ToTable("Viajes");
            builder.HasKey(x => x.viaje_id);
            builder.Property(x => x.fecha).IsRequired();
            builder.Property(x => x.distancia_recorrida_km).HasColumnType("decimal(10,2)").IsRequired();
            builder.Property(x => x.total_pagar).HasColumnType("decimal(10,2)").IsRequired();
            builder.Property(x => x.sucursal_id).IsRequired();
            builder.Property(x => x.usuario_id).IsRequired();
            builder.Property(x => x.transportista_id).IsRequired();

            builder.Property(x => x.usuario_creacion).IsRequired();
            builder.Property(x => x.fecha_creacion).IsRequired();
            builder.Property(x => x.usuario_modificacion).IsRequired(false);
            builder.Property(x => x.fecha_modificacion).IsRequired(false);
            builder.Property(x => x.es_activo).IsRequired();

            builder.HasOne(x => x.Sucursal)
                .WithMany(x => x.Viajes)
                .HasForeignKey(x => x.sucursal_id);

            builder.HasOne(x => x.Transportista)
                .WithMany(x => x.Viajes)
                .HasForeignKey(x => x.transportista_id);

            builder.HasOne(x => x.Usuario)
                .WithMany(x => x.ViajesEncabezado)
                .HasForeignKey(x => x.usuario_id);

            builder.HasOne(x => x.UsuarioCrear)
                .WithMany(x => x.ViajesCreacion)
                .HasForeignKey(x => x.usuario_creacion);

            builder.HasOne(x => x.UsuarioModificar)
                .WithMany(x => x.ViajesModifiacion)
                .HasForeignKey(x => x.usuario_modificacion);
        }
    }
}
