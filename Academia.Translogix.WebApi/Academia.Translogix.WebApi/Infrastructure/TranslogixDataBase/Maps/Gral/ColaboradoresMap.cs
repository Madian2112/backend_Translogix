using Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase.Entities.Gral;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase.Maps.Gral
{
    public class ColaboradoresMap : IEntityTypeConfiguration<Colaboradores>
    {
        public void Configure(EntityTypeBuilder<Colaboradores> builder)
        {
            builder.ToTable("Colaboradores");
            builder.HasKey(x => x.colaborador_id);
            builder.Property(x => x.fecha_nacimiento).IsRequired();
            builder.Property(x => x.latitud).HasColumnType("decimal(19,15)").IsRequired();
            builder.Property(x => x.longitud).HasColumnType("decimal(19,15)").IsRequired();
            builder.Property(x => x.estado_civil_id).IsRequired();
            builder.Property(x => x.cargo_id).IsRequired();
            builder.Property(x => x.persona_id).IsRequired();

            builder.Property(x => x.usuario_creacion).IsRequired();
            builder.Property(x => x.fecha_creacion).IsRequired();
            builder.Property(x => x.usuario_modificacion).IsRequired(false);
            builder.Property(x => x.fecha_modificacion).IsRequired(false);
            builder.Property(x => x.es_activo).IsRequired();

            builder.HasOne(x => x.EstadoCivil)
                .WithMany(x => x.Colaboradores)
                .HasForeignKey(x => x.estado_civil_id);

            builder.HasOne(x => x.Cargo)
                .WithMany(x => x.Colaboradores)
                .HasForeignKey(x => x.cargo_id);

            builder.HasOne(x => x.Persona)
                .WithMany(x => x.Colaboradores)
                .HasForeignKey(x => x.persona_id);

            builder.HasOne(x => x.UsuarioCrear)
                .WithMany(x => x.ColaboradoresCreacion)
                .HasForeignKey(x => x.usuario_creacion);

            builder.HasOne(x => x.UsuarioModificar)
                .WithMany(x => x.ColaboradoresModificacion)
                .HasForeignKey(x => x.usuario_modificacion);
        }
    }
}
