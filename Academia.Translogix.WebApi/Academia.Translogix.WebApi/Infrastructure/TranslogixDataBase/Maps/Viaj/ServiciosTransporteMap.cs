using Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase.Entities.Gral;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase.Maps.Viaj
{
    public class ServiciosTransporteMap : IEntityTypeConfiguration<ServiciosTransporte>
    {
        public void Configure(EntityTypeBuilder<ServiciosTransporte> builder)
        {
            builder.ToTable("Servicios_Transporte");
            builder.HasKey(x => x.servicio_transporte_id);
            builder.Property(x => x.nombre).HasMaxLength(150).IsRequired();
            builder.Property(x => x.telefono).HasMaxLength(50).IsRequired();
            builder.Property(x => x.correo_electronico).HasMaxLength(250).IsRequired();

            builder.Property(x => x.usuario_creacion).IsRequired();
            builder.Property(x => x.fecha_creacion).IsRequired();
            builder.Property(x => x.usuario_modificacion).IsRequired(false);
            builder.Property(x => x.fecha_modificacion).IsRequired(false);
            builder.Property(x => x.es_activo).IsRequired();

            builder.HasOne(x => x.UsuarioCrear)
            .WithMany(x => x.ServiciosTransporteCreacion)
            .HasForeignKey(x => x.usuario_creacion);

            builder.HasOne(x => x.UsuarioModificar)
            .WithMany(x => x.ServiciosTransporteModifiacion)
            .HasForeignKey(x => x.usuario_modificacion);
        }
    }
}
