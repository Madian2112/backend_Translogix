using Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase.Entities.Viaj;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase.Maps.Viaj
{
    public class TransportistasMap : IEntityTypeConfiguration<Transportistas>
    {
        public void Configure(EntityTypeBuilder<Transportistas> builder)
        {
            builder.ToTable("Transportistas");
            builder.HasKey(x => x.tarifa_id);
            builder.Property(x => x.persona_id).IsRequired();
            builder.Property(x => x.tarifa_id).IsRequired();
            builder.Property(x => x.moneda_id).IsRequired();

            builder.Property(x => x.usuario_creacion).IsRequired();
            builder.Property(x => x.fecha_creacion).IsRequired();
            builder.Property(x => x.usuario_modificacion).IsRequired(false);
            builder.Property(x => x.fecha_modificacion).IsRequired(false);
            builder.Property(x => x.es_activo).IsRequired();

            builder.HasOne(x => x.Persona)
                .WithMany(x => x.Transportistas)
                .HasForeignKey(x => x.persona_id);

            builder.HasOne(x => x.Tarifa)
                .WithMany(x => x.Transportistas)
                .HasForeignKey(x => x.tarifa_id);

            builder.HasOne(x => x.Moneda)
                .WithMany(x => x.Transportistas)
                .HasForeignKey(x => x.moneda_id);

            builder.HasOne(x => x.UsuarioCrear)
                .WithMany(x => x.TransportistasCreacion)
                .HasForeignKey(x => x.usuario_creacion);

            builder.HasOne(x => x.UsuarioModificar)
                .WithMany(x => x.TransportistasModifiacion)
                .HasForeignKey(x => x.usuario_modificacion);
        }
    }
}
