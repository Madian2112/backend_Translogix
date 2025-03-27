using Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase.Entities.Viaj;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase.Maps.Viaj
{
    public class TarifasMap : IEntityTypeConfiguration<Tarifas>
    {
        public void Configure(EntityTypeBuilder<Tarifas> builder)
        {
            builder.ToTable("Tarifas");
            builder.HasKey(x => x.tarifa_id);
            builder.Property(x => x.precio_km).HasColumnType("decimal(10,2)").IsRequired();

            builder.Property(x => x.usuario_creacion).IsRequired();
            builder.Property(x => x.fecha_creacion).IsRequired();
            builder.Property(x => x.usuario_modificacion).IsRequired(false);
            builder.Property(x => x.fecha_modificacion).IsRequired(false);
            builder.Property(x => x.es_activo).IsRequired();

            builder.HasOne(x => x.UsuarioCrear)
                .WithMany(x => x.TarifasCreacion)
                .HasForeignKey(x => x.usuario_creacion);

            builder.HasOne(x => x.UsuarioModificar)
                .WithMany(x => x.TarifasModifiacion)
                .HasForeignKey(x => x.usuario_modificacion);
        }
    }
}
