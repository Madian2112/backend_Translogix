using Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase.Maps
{
    public class ColaboradorMap : IEntityTypeConfiguration<Colaboradores>
    {
        public void Configure(EntityTypeBuilder<Colaboradores> builder)
        {
            builder.ToTable("Colaboradores");
            builder.HasKey(x => x.colaborador_id);
            builder.Property(x => x.fecha_nacimiento).IsRequired();
            builder.Property(x => x.latitud).IsRequired();
            builder.Property(x => x.longitud).IsRequired();
            builder.Property(x => x.estado_civil_id).IsRequired();
            builder.Property(x => x.cargo_id).IsRequired();
            builder.Property(x => x.persona_id).IsRequired();
            builder.Property(x => x.usuario_creacion).IsRequired();
            builder.Property(x => x.fecha_creacion).IsRequired();
            builder.Property(x => x.usuario_modificacion);
            builder.Property(x => x.fecha_modificacion);
            builder.Property(x => x.es_activo).IsRequired();

            builder.HasOne(x => x.Estados_Civiles)
                .WithMany(x => x.Colaboradores)
                .HasForeignKey(x => x.estado_civil_id);

            builder.HasOne(x => x.Cargos)
                .WithMany(x => x.Colaboradores)
                .HasForeignKey(x => x.cargo_id);

            builder.HasOne(x => x.Personas)
                .WithMany(x => x.Colaboradores)
                .HasForeignKey(x => x.persona_id);

            builder.HasOne(x => x.UsuarioCrear)
                .WithMany(x => x.Colaboradores)
                .HasForeignKey(x => x.usuario_creacion);

            builder.HasOne(x => x.UsuarioModificar)
                .WithMany(x => x.Colaboradores)
                .HasForeignKey(x => x.usuario_modificacion);
        }
    }
}
