using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Academia.Translogix.WebApi._Features.Viaj.Dtos
{
    public class RolesDto
    {
        public int rol_id { get; set; }
        public string nombre { get; set; } = string.Empty;
        public string descripcion { get; set; } = string.Empty;
        public bool es_activo { get; set; }
    }

    public class RolesDtoInsertar
    {
        public string nombre { get; set; } = string.Empty;
        public string descripcion { get; set; } = string.Empty;
        public int usuario_creacion { get; set; }
        public DateTime fecha_creacion { get; set; }
        public bool es_activo { get; set; }
    }

    public class RolesDtoActualizar
    {
        public int rol_id { get; set; }
        public string nombre { get; set; } = string.Empty;
        public string descripcion { get; set; } = string.Empty;
        public int? usuario_modificacion { get; set; }
        public DateTime? fecha_modificacion { get; set; }
    }
}