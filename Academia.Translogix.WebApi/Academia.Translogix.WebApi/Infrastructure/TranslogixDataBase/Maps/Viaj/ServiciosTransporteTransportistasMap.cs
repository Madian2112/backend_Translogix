using Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase.Entities.Gral;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase.Maps.Viaj
{
    public class ServiciosTransporteTransportistasMap : IEntityTypeConfiguration<Servicios_Transporte_Transportistas>
    {
        public void Configure(EntityTypeBuilder<Servicios_Transporte_Transportistas> builder)
        {
            builder.ToTable("Servicios_Transporte_Transportistas");
            builder.HasKey(x => x.servicio_transporte_transportista_id);
            builder.Property(x => x.servicio_transporte_id).IsRequired();
            builder.Property(x => x.transportista_id).IsRequired();

            builder.Property(x => x.usuario_creacion).IsRequired();
            builder.Property(x => x.fecha_creacion).IsRequired();
            builder.Property(x => x.usuario_modificacion).IsRequired(false);
            builder.Property(x => x.fecha_modificacion).IsRequired(false);
            builder.Property(x => x.es_activo).IsRequired();

            builder.HasOne(x => x.ServiciosTransporte)
            .WithMany(x => x.ServiciosTransporteTransportistas)
            .HasForeignKey(x => x.servicio_transporte_id);

            builder.HasOne(x => x.Transportista)
            .WithMany(x => x.ServiciosTransporteTransportistas)
            .HasForeignKey(x => x.transportista_id);

            builder.HasOne(x => x.UsuarioCrear)
            .WithMany(x => x.ServiciosTransporteTransportistasCreacion)
            .HasForeignKey(x => x.usuario_creacion);

            builder.HasOne(x => x.UsuarioModificar)
            .WithMany(x => x.ServiciosTransporteTransportistasModifiacion)
            .HasForeignKey(x => x.usuario_modificacion);
        }
    }
}
