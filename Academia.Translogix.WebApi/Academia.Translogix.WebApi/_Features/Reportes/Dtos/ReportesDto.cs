namespace Academia.Translogix.WebApi._Features.Reportes.Dtos
{
    public class ReporteDto
    {
        public int? viajeEncabezadoId { get; set; }
        public string? Sucursal { get; set; }
        public string? UsuarioCreacion { get; set; }
        public string? Colaborador { get; set; }
        public string? Transportista { get; set; }
        public decimal? TotalAPagar { get; set; }
        public double? TotalDeKilometros { get; set; }
        public decimal? GranTotalAPagar { get; set; }
        public double? GranTotalDeKilometros { get; set; }
        public int transportistaId { get; set; }
        public DateTime? fechaCreacion { get; set; }
    }
    public class ReporteInsertarDto
    {
        public DateTime fechaInicio { get; set; }
        public DateTime fechaFin { get; set; }
        public int transportista_id { get; set; }
    }
}
