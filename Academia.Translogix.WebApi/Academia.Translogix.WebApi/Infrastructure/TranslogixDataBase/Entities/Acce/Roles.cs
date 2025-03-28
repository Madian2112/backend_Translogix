﻿using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase.Entities.Acce
{
    [ExcludeFromCodeCoverage]
    public class Roles
    {
        [Key]
        public int rol_id { get; set; }
        public string nombre { get; set; } = string.Empty;
        public string descripcion { get; set; } = string.Empty;
        public int usuario_creacion { get; set; }
        public DateTime fecha_creacion { get; set; }
        public int? usuario_modificacion { get; set; }
        public DateTime? fecha_modificacion { get; set; }
        public bool es_activo { get; set; }

        public Usuarios UsuarioCrear { get; set; } = null!;
        public Usuarios? UsuarioModificar { get; set; }
        public ICollection<Usuarios> Usuarios { get; set; } = new List<Usuarios>();
    }
}
