using Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase.Entities.Acce;
using Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase.Entities.Gral;

namespace Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase.Entities.Viaj
{
    public class Sucursales_Colaboradores
    {
        public int sucursal_empleado_id { get; set; }
        public decimal distancia_empleado_sucursal_km { get; set; }
        public int colaborador_id { get; set; }
        public int sucursal_id { get; set; }
        public int usuario_creacion { get; set; }
        public DateTime fecha_creacion { get; set; }
        public int? usuario_modificacion { get; set; }
        public DateTime? fecha_modificacion { get; set; }
        public bool es_activo { get; set; }

        public Colaboradores Colaborador { get; set; } = null!;
        public Sucursales Sucursal { get; set; } = null!;
        public Usuarios UsuarioCrear { get; set; } = null!;
        public Usuarios? UsuarioModificar { get; set; }


    }
}
