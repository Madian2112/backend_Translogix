using Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase.Entities.Acce;
using Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase.Entities.Viaj;

namespace Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase.Entities.Gral
{
    public class Monedas
    {
        public int moneda_id { get; set; }
        public string nombre { get; set; } = string.Empty;
        public string abreviatura { get; set; } = string.Empty;
        public decimal valor_lempira { get; set; }
        public int? pais_id { get; set; }
        public int usuario_creacion { get; set; }
        public DateTime fecha_creacion { get; set; }
        public int? usuario_modificacion { get; set; }
        public DateTime? fecha_modificacion { get; set; }
        public bool? es_activo { get; set; }

        public Paises Pais { get; set; } = null!;
        public Usuarios UsuarioCrear { get; set; } = null!;
        public Usuarios? UsuarioModificar { get; set; }
        public ICollection<Transportistas> Transportistas { get; set; } = new List<Transportistas>();

    }
}
