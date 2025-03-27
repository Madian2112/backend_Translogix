using Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase.Entities.Gral;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase.Maps.Gral
{
    public class PersonasMap : IEntityTypeConfiguration<Personas>
    {
        public void Configure(EntityTypeBuilder<Personas> builder)
        {
            builder.ToTable("Personas");
            builder.HasKey(x => x.persona_id);
            builder.Property(x => x.identidad).HasMaxLength(50).IsRequired();
            builder.Property(x => x.primer_nombre).HasMaxLength(70).IsRequired();
            builder.Property(x => x.segundo_nombre).HasMaxLength(70).IsRequired();
            builder.Property(x => x.primer_apellido).HasMaxLength(70).IsRequired();
            builder.Property(x => x.segundo_apellido).HasMaxLength(70).IsRequired();
            builder.Property(x => x.sexo).HasMaxLength(1).IsRequired();
            builder.Property(x => x.telefono).HasMaxLength(100).IsRequired();
            builder.Property(x => x.correo_electronico).HasMaxLength(250).IsRequired();
            builder.Property(x => x.pais_id).IsRequired();

            builder.Property(x => x.usuario_creacion).IsRequired();
            builder.Property(x => x.fecha_creacion).IsRequired();
            builder.Property(x => x.usuario_modificacion).IsRequired(false);
            builder.Property(x => x.fecha_modificacion).IsRequired(false);
            builder.Property(x => x.es_activo).IsRequired();

            builder.HasOne(x => x.Pais)
            .WithMany(x => x.Personas)
            .HasForeignKey(x => x.pais_id);

            builder.HasOne(x => x.UsuarioCrear)
            .WithMany(x => x.PersonasCreacion)
            .HasForeignKey(x => x.usuario_creacion);

            builder.HasOne(x => x.UsuarioModificar)
            .WithMany(x => x.PersonasModifiacion)
            .HasForeignKey(x => x.usuario_modificacion);
        }
    }
}
