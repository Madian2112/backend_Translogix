using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Academia.Translogix.WebApi._Features.Gral.Dtos
{
    public class CargosDto
    {
        public int cargo_id { get; set; }
        public string nombre { get; set; } = string.Empty;
        public bool es_activo { get; set; }
    }

    public class CargosDtoInsertar
    {
        public string nombre { get; set; } = string.Empty;
        public int usuario_creacion { get; set; }
        public DateTime fecha_creacion { get; set; }
        public bool es_activo { get; set; }
    }

    public class CargosDtoActualizar
    {
        public int cargo_id { get; set; }
        public string nombre { get; set; } = string.Empty;
        public int? usuario_modificacion { get; set; }
        public DateTime? fecha_modificacion { get; set; }
        public bool es_activo { get; set; }
    }
}