using Academia.Translogix.WebApi._Features.Gral.Dtos;

namespace Academia.Translogix.WebApi._Features.Viaj.Dtos
{
    public class TransportistasDto
    {
        public class TransportistaDto
        {
            public int transportista_id { get; set; }
            public int? moneda_id { get; set; }
            public int persona_id { get; set; }
            public int tarifa_id { get; set; }
            public bool es_activo { get; set; }
            public TarifasDto Tarifa { get; set; }
            public PersonasDto Persona { get; set; }
        }

        public class TransportistaInsertar
        {
            public int moneda_id { get; set; }
            public int persona_id { get; set; }
            public int tarifa_id { get; set; }
            public int usuario_creacion { get; set; }
            public DateTime fecha_creacion { get; set; } = DateTime.Now;
            public bool es_activo { get; set; } = true;
        }

        public class TransportistaInsertarDto
        {
            public TransportistaInsertar Transportista { get; set; } = new TransportistaInsertar();
            public PersonasDtoInsertar Persona { get; set; } = new PersonasDtoInsertar();

        }
    }
}
