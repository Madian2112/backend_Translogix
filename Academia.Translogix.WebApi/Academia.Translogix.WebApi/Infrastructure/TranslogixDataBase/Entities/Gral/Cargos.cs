﻿using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase.Entities.Acce;
using Microsoft.EntityFrameworkCore;

namespace Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase.Entities.Gral
{
    [ExcludeFromCodeCoverage]
    public class Cargos
    {
        [Key]
        public int cargo_id { get; set; }
        public string nombre { get; set; } = string.Empty;
        public int usuario_creacion { get; set; }
        public DateTime fecha_creacion { get; set; }
        public int? usuario_modificacion { get; set; }
        public DateTime? fecha_modificacion { get; set; }
        public bool es_activo { get; set; }

        public Usuarios UsuarioCrear { get; set; } = null!;
        public Usuarios? UsuarioModificar { get; set; }
        public ICollection<Colaboradores> Colaboradores { get; set; } = new List<Colaboradores>();

    }
}
