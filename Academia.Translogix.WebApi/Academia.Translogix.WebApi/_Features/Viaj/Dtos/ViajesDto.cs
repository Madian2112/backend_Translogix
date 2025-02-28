using System.Text.Json.Serialization;

namespace Academia.Translogix.WebApi._Features.Viaj.Dtos
{
    public class ViajesDto
    {
        public int viaje_id { get; set; }
        public DateTime fecha { get; set; }
        public decimal distancia_recorrida_km { get; set; }
        public decimal total_pagar { get; set; }
        public int sucursal_id { get; set; }
        public int usuario_id { get; set; }
        public bool es_activo { get; set; }
    }
        public class ViajesModeloInsertarDto
        {
            public DateTime fecha_hora { get; set; } = DateTime.Now;
            public int usuario_creacion { get; set; } 
            public bool es_activo { get; set; } = true;
            public Coordinate Origen { get; set; }
            public List<Transportista> transportista { get; set; }
            public List<Ubicacion> Ubicaciones { get; set; }
        }

        public class ViajesInsertarDto
        {
            public  DateTime fecha { get; set; }
            public decimal distancia_recorrida_km { get; set; }
            public decimal total_pagar { get; set; }
            public int sucursal_id { get; set; }
            public int usuario_id { get; set; }
            public int transportista_id { get; set; }
            public int usuario_creacion { get; set; }
            public DateTime fecha_creacion { get; set; }
            public bool es_activo { get; set; } = true;
        }

        //public class ViajeInsertarDto
        //{
        //    public Coordinate Origen { get; set; }
        //    public Transportista transportista { get; set; }
        //    public List<Ubicacion> Ubicaciones { get; set; }
        //}

        public class Transportista
        {
            public int transportista_id { get; set; }
            //public int tarifa_id { get; set; }
            public decimal precio_km { get; set; }
        }

        public class UbicacionesViaje
        {
            public double[] Ubicaciones { get; set; }
            public double[] Transportistas { get; set; }
            public decimal DistanciaKm { get; set; }
            public int colaborador_id { get; set; }
        }

        public class Ubicacion
        {
            public double Latitud { get; set; }
            public double Longitud { get; set; }
            public decimal DistanciaKm { get; set; }
            public int colaborador_id { get; set; }
        }

        public class Coordinate
        {
            public int sucursal_id { get; set; }
            public double Latitude { get; set; }
            public double Longitude { get; set; }
        }

        public class RouteResponse
        {
            public List<Route> Routes { get; set; }
        }
        public class Route
        {
            public Summary Summary { get; set; }
            public string Geometry { get; set; }
            public List<Waypoint> Waypoints { get; set; }
            public List<Segment> Segments { get; set; }
        }
        public class Summary
        {
            public double Distance { get; set; }
            public double Duration { get; set; }
        }
        public class Waypoint
        {
            public double[] Location { get; set; }
            public int WaypointIndex { get; set; }
        }
        public class Segment
        {
            public List<Step> Steps { get; set; }
        }
        public class Step
        {
            public string Instruction { get; set; }
            public double Distance { get; set; }
        }
        public class AgruparRutasResponse
        {
            public List<RutaAgrupadaResponse> RutasAgrupadas { get; set; } = new List<RutaAgrupadaResponse>();
            public string Mensaje { get; set; } = string.Empty;
        }

        public class RutaAgrupadaResponse
        {
            public double Distance { get; set; }
            public double Duration { get; set; }
            public string Geometry { get; set; }
            public List<Waypoint>? Waypoints { get; set; }
            public List<List<Step>> Steps { get; set; } = new List<List<Step>>();
            public double DistanciaMaximaDesdeOrigen { get; set; }
            public decimal DistanciaAcumuladaKm { get; set; }
            public decimal TotalPagoPorViaje { get; set; }
            public int transportista_id { get; set; }
            public List<ColaboradorInfo> Colaboradores { get; set; } = new List<ColaboradorInfo>();
        }
        public class ColaboradorInfo
        {
            public int ColaboradorId { get; set; }
            public decimal DistanciaKm { get; set; }
            public decimal TotalPagoPorColaborador { get; set; }
        }
}
