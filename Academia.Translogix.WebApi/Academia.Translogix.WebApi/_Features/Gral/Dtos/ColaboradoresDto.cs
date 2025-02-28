using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Academia.Translogix.WebApi._Features.Gral.Dtos
{
    public class ColaboradoresDto
    {
        public int colaborador_id { get; set; }
        public DateTime fecha_nacimiento { get; set; }
        public decimal latitud { get; set; }
        public decimal longitud { get; set; }
        public int estado_civil_id { get; set; }
        public int cargo_id { get; set; }
        public int persona_id { get; set; }
        public bool es_activo { get; set; }
    }

    public class ColaboradoresInsertar
    {
        public DateTime fecha_nacimiento { get; set; } = DateTime.Now;
        public decimal latitud { get; set; }
        public decimal longitud { get; set; }
        public int estado_civil_id { get; set; }
        public int cargo_id { get; set; }
        public int persona_id { get; set; }
        public int usuario_creacion { get; set; }
        public DateTime fecha_creacion { get; set; } = DateTime.Now;
        public bool es_activo { get; set; }
    }

    public class ColaboradoresDtoInsertar
    {
        public ColaboradoresInsertar Colaborador { get; set; } = new ColaboradoresInsertar();
        public PersonasDtoInsertar Persona { get; set; } = new PersonasDtoInsertar();
    }

    public class ColaboradoresDtoActualizar
    {
        public DateTime fecha_nacimiento { get; set; } = DateTime.Now;
        public decimal latitud { get; set; }
        public decimal longitud { get; set; }
        public int estado_civil_id { get; set; }
        public int cargo_id { get; set; }
        public int persona_id { get; set; }
        public int usuario_modificacion { get; set; }
        public DateTime fecha_modificacion { get; set; } = DateTime.Now;
    }

    public class ColaboradoresSucursales
    {
        public int colaborador_id { get; set; }
        public int sucursal_id { get; set; }
        public decimal latitudColaborador { get; set; }
        public decimal longitudColaborador { get; set; }
        public decimal latitudSucursal { get; set; }
        public decimal longitudSucursal { get; set; }
        public decimal distancia_empleado_sucursal_km { get; set; }
    }

    public class ColaboradoresSucursalesNoAsignadosDto
    {
        public int colaborador_id { get; set; } 
        public string identidad { get; set; } = string.Empty;
        public string primer_nombre { get; set; } = string.Empty;
        public string primer_apellido { get; set; } = string.Empty;
    }

    public class ColaboradoresSinViaje
    {
        public int colaborador_id { get; set; }
        public string identidad { get; set; } = string.Empty;
        public string primer_nombre { get; set; } = string.Empty;
        public string segundo_nombre { get; set; } = string.Empty;
        public string primer_apellido { get; set; } = string.Empty;
        public string segundo_apellido { get; set; } = string.Empty;
        public decimal latitud { get; set; }
        public decimal longitud { get; set; }
        public decimal distancia_empleado_sucursal_km { get; set; }
    }
}