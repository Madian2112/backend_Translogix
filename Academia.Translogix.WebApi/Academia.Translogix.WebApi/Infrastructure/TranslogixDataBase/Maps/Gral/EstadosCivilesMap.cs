using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase.Entities.Gral;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase.Maps.Gral
{
    public class EstadosCivilesMap : IEntityTypeConfiguration<Estados_Civiles>
    {
        public void Configure(EntityTypeBuilder<Estados_Civiles> builder)
        {
            builder.ToTable("Estados_Civiles");
            builder.HasKey(x => x.estado_civil_id);
            builder.Property(x => x.nombre).HasMaxLength(50).IsRequired();

            builder.Property(x => x.usuario_creacion).IsRequired();
            builder.Property(x => x.fecha_creacion).IsRequired();
            builder.Property(x => x.usuario_modificacion).IsRequired(false);
            builder.Property(x => x.fecha_modificacion).IsRequired(false);
            builder.Property(x => x.es_activo).IsRequired();

            builder.HasOne(x => x.UsuarioCrear)
            .WithMany(x => x.EstadosCivilesCreacion)
            .HasForeignKey(x => x.usuario_creacion);

            builder.HasOne(x => x.UsuarioModificar)
            .WithMany(x => x.EstadosCivilesModificacion)
            .HasForeignKey(x => x.usuario_modificacion);
        }
    }
}