using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase.Entities.Acce;

namespace Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase.Entities.Viaj
{
    [ExcludeFromCodeCoverage]
    public class Sucursales
    {
        [Key]
        public int sucursal_id { get; set; }
        public string nombre { get; set; } = string.Empty;
        public int usuario_creacion { get; set; }
        public DateTime fecha_creacion { get; set; }
        public int? usuario_modificacion { get; set; }
        public DateTime? fecha_modificacion { get; set; }
        public bool es_activo { get; set; }
        public decimal latitud { get; set; }
        public decimal longitud { get; set; }

        public Usuarios UsuarioCrear { get; set; } = null!;
        public Usuarios? UsuarioModificar { get; set; }
        public ICollection<SucursalesColaboradores> SucursalesColaboradores { get; set; } = new List<SucursalesColaboradores>();
        public ICollection<Viajes> Viajes { get; set; } = new List<Viajes>();
    }
}
