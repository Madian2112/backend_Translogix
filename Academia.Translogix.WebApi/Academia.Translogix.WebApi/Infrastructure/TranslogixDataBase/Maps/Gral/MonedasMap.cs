using Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase.Entities.Gral;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase.Maps.Gral
{
    public class MonedasMap : IEntityTypeConfiguration<Monedas>
    {
        public void Configure(EntityTypeBuilder<Monedas> builder)
        {
            builder.ToTable("Monedas");
            builder.HasKey(x => x.moneda_id);
            builder.Property(x => x.nombre).HasMaxLength(100).IsRequired();
            builder.Property(x => x.abreviatura).HasMaxLength(10).IsRequired();
            builder.Property(x => x.valor_lempira).HasColumnType("decimal(20,3)").IsRequired();
            builder.Property(x => x.pais_id).IsRequired();

            builder.Property(x => x.usuario_creacion).IsRequired();
            builder.Property(x => x.fecha_creacion).IsRequired();
            builder.Property(x => x.usuario_modificacion).IsRequired(false);
            builder.Property(x => x.fecha_modificacion).IsRequired(false);
            builder.Property(x => x.es_activo).IsRequired();

            builder.HasOne(x => x.Pais)
                .WithMany(x => x.Monedas)
                .HasForeignKey(x => x.pais_id);

            builder.HasOne(x => x.UsuarioCrear)
                .WithMany(x => x.MonedasCreacion)
                .HasForeignKey(x => x.usuario_creacion);

            builder.HasOne(x => x.UsuarioModificar)
                .WithMany(x => x.MonedasModificacion)
                .HasForeignKey(x => x.usuario_modificacion);
        }
    }
}