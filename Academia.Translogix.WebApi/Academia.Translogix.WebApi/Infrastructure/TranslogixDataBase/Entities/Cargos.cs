﻿namespace Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase.Entities
{
    public class Cargos
    {
        public int cargo_id { get; set; }
        public string nombre { get; set; }
        public int usuario_creacion { get; set; }
        public DateTime fecha_creacion { get; set; }
        public int? usuario_modificacion { get; set; }
        public DateTime? fecha_modificacion { get; set; }
        public bool es_activo { get; set; }

        public ICollection<Colaboradores> Colaboradores { get; set; }

    }
}
