using Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase.Entities.Viaj;

namespace Academia.Translogix.WebApi._Features.Reportes.Service
{
    public class ReporteDominioService
    {
        public bool esNulo(Transportistas transportistas)
        {
            return transportistas == null ? false : true;
        }
        public bool ValidarFechas(DateTime? fechaInicio, DateTime? fechaFin)
        {
            return fechaInicio.HasValue && fechaFin.HasValue && fechaInicio > fechaFin ? false : true;
        }

        public bool FiltrarPorFechas(DateTime? fechaCreacion, DateTime? fechaInicio, DateTime? fechaFin)
        {
            return (!fechaInicio.HasValue || fechaCreacion >= fechaInicio.Value) &&
                   (!fechaFin.HasValue || fechaCreacion <= fechaFin.Value);
        }

        public bool FiltrarPorTransportista(int transportistaId, int? filtroTransportistaId)
        {
            return !filtroTransportistaId.HasValue || transportistaId == filtroTransportistaId.Value;
        }
    }
}
