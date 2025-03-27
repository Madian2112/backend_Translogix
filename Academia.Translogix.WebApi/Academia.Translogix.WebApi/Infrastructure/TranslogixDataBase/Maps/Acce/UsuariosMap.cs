using Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase.Entities.Acce;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase.Maps.Acce
{
    public class UsuariosMap : IEntityTypeConfiguration<Usuarios>
    {
        void IEntityTypeConfiguration<Usuarios>.Configure(EntityTypeBuilder<Usuarios> builder)
        {
            builder.ToTable("Usuarios");
            builder.HasKey(x => x.usuario_id);
            builder.Property(x => x.nombre).HasMaxLength(150).IsRequired();
            builder.Property(x => x.clave).IsRequired();
            builder.Property(x => x.es_admin).IsRequired();
            builder.Property(x => x.colaborador_id).IsRequired();
            builder.Property(x => x.rol_id).IsRequired();

            builder.Property(x => x.usuario_creacion).IsRequired();
            builder.Property(x => x.fecha_creacion).IsRequired();
            builder.Property(x => x.usuario_modificacion).IsRequired(false);
            builder.Property(x => x.fecha_modificacion).IsRequired(false);
            builder.Property(x => x.es_activo).IsRequired();

            builder.HasOne(x => x.Rol)
                .WithMany(x => x.Usuarios)
                .HasForeignKey(x => x.rol_id);

            builder.HasOne(x => x.Colaborador)
                .WithMany(x => x.Usuarios)
                .HasForeignKey(x => x.colaborador_id);

            builder.HasOne(x => x.UsuarioCrear)
                .WithMany(x => x.UsuariosCreacion)
                .HasForeignKey(x => x.usuario_creacion);

            builder.HasOne(x => x.UsuarioModificar)
                .WithMany(x => x.UsuariosModifiacion)
                .HasForeignKey(x => x.usuario_modificacion);
        }
    }
}
