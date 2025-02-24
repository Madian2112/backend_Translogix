using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Academia.Translogix.WebApi._Features.Gral.Dtos
{
    public class EstadosCivilesDto
    {
        public int estado_civil_id { get; set; }
        public string nombre { get; set; } = string.Empty;
        public bool es_activo { get; set; }
    }

    public class EstadosCivilesDtoInsertar
    {
        public string nombre { get; set; } = string.Empty;
        public bool es_activo { get; set; }
        public DateTime fecha_creacion { get; set; }
        public int usuario_creacion { get; set; }
    }

    public class EstadosCivilesDtoActualizar
    {
        public int estado_civil_id { get; set; }
        public string nombre { get; set; } = string.Empty;
        public DateTime fecha_modificacion { get; set; }
        public int usuario_modificacion { get; set; }
    }
}