using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase.Entities.Gral;
using System.Diagnostics.CodeAnalysis;


namespace Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase.Maps.Gral
{
    public class CargosMap : IEntityTypeConfiguration<Cargos>
    {
        public void Configure(EntityTypeBuilder<Cargos> builder)
        {
            builder.ToTable("Cargos");
            builder.HasKey(x => x.cargo_id);
            builder.Property(x => x.nombre).HasMaxLength(100).IsRequired();

            builder.Property(x => x.usuario_creacion).IsRequired();
            builder.Property(x => x.fecha_creacion).IsRequired();
            builder.Property(x => x.usuario_modificacion).IsRequired(false);
            builder.Property(x => x.fecha_modificacion).IsRequired(false);
            builder.Property(x => x.es_activo).IsRequired();

            builder.HasOne(x => x.UsuarioCrear)
                .WithMany(x => x.CargosCreacion)
                .HasForeignKey(x => x.usuario_creacion);

            builder.HasOne(x => x.UsuarioModificar)
                .WithMany(x => x.CargosModificacion)
                .HasForeignKey(x => x.usuario_modificacion);
        }
    }
}