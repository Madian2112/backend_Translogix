using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase.Maps
{
    public class PaisMap : IEntityTypeConfiguration<Paises>
    {
        public void Configure(EntityTypeBuilder<Paises> builder)
        {
            builder.ToTable("Paises");
            builder.HasKey(x => x.pais_id);
            builder.Property(x => x.prefijo).HasMaxLength(10).IsRequired();
            builder.Property(x => x.nombre).HasMaxLength(100).IsRequired();
            builder.Property(x => x.pais_id).IsRequired();
            builder.Property(x => x.usuario_creacion).IsRequired();
            builder.Property(x => x.fecha_creacion).IsRequired();
            builder.Property(x => x.usuario_modificacion).IsRequired(false);
            builder.Property(x => x.fecha_modificacion).IsRequired(false);
            builder.Property(x => x.es_activo).IsRequired();

            builder.HasOne(x => x.UsuarioCrear)
            .WithMany(x => x.PaisesCreacion)
            .HasForeignKey(x => x.usuario_creacion);

            builder.HasOne(x => x.UsuarioModificar)
            .WithMany(x => x.PaisesModifiacion)
            .HasForeignKey(x => x.usuario_modificacion);
        }
    }
}