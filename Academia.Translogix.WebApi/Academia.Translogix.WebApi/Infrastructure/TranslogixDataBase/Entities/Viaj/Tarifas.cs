using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase.Entities.Acce;

namespace Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase.Entities.Viaj
{
    [ExcludeFromCodeCoverage]
    public class Tarifas
    {
        [Key]
        public int tarifa_id { get; set; }
        public decimal precio_km { get; set; }
        public int usuario_creacion { get; set; }
        public DateTime fecha_creacion { get; set; }
        public int? usuario_modificacion { get; set; }
        public DateTime? fecha_modificacion { get; set; }
        public bool es_activo { get; set; }

        public Usuarios UsuarioCrear { get; set; } = null!;
        public Usuarios? UsuarioModificar { get; set; }
        public ICollection<Transportistas> Transportistas { get; set; } = new List<Transportistas>();


    }
}
