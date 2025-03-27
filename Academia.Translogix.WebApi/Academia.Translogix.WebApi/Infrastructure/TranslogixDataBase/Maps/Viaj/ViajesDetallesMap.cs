using Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase.Entities.Viaj;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase.Maps.Viaj
{
    public class ViajesDetallesMap : IEntityTypeConfiguration<ViajesDetalles>
    {
        public void Configure(EntityTypeBuilder<ViajesDetalles> builder)
        {
            builder.ToTable("Viajes_Detalles");
            builder.HasKey(x => x.viaje_detalle_id);
            builder.Property(x => x.total_pagar_por_km).HasColumnType("decimal(10,2)").IsRequired();
            builder.Property(x => x.viaje_id).IsRequired();
            builder.Property(x => x.colaborador_id).IsRequired();

            builder.Property(x => x.usuario_creacion).IsRequired();
            builder.Property(x => x.fecha_creacion).IsRequired();
            builder.Property(x => x.usuario_modificacion).IsRequired(false);
            builder.Property(x => x.fecha_modificacion).IsRequired(false);
            builder.Property(x => x.es_activo).IsRequired();

            builder.HasOne(x => x.Viaje)
                .WithMany(x => x.ViajesDetalles)
                .HasForeignKey(x => x.viaje_id);

            builder.HasOne(x => x.Colaborador)
                .WithMany(x => x.ViajesDetalles)
                .HasForeignKey(x => x.colaborador_id);

            builder.HasOne(x => x.UsuarioCrear)
                .WithMany(x => x.ViajesDetallesCreacion)
                .HasForeignKey(x => x.usuario_creacion);

            builder.HasOne(x => x.UsuarioModificar)
                .WithMany(x => x.ViajesDetallesModifiacion)
                .HasForeignKey(x => x.usuario_modificacion);
        }
    }
}
