using Academia.Translogix.WebApi._Features.Reportes.Dtos;
using Academia.Translogix.WebApi.Common;
using Academia.Translogix.WebApi.Common._ApiResponses;
using Academia.Translogix.WebApi.Common._BaseDomain;
using Academia.Translogix.WebApi.Infrastructure;
using Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase;
using Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase.Entities.Gral;
using Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase.Entities.Viaj;
using Farsiman.Domain.Core.Standard.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Academia.Translogix.WebApi._Features.Reportes.Service
{
    public class ReporteService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly TranslogixDBContext _context;
        private readonly ReporteDominioService _dominioService;
        public ReporteService(UnitOfWorkBuilder unitOfWork, TranslogixDBContext context, ReporteDominioService dominioService)
        {
            _unitOfWork = unitOfWork.BuildDbTranslogix();
            _context = context;
            _dominioService = dominioService;
        }

        public ApiResponse<List<ReporteDto>> CargarReporte(ReporteInsertarDto reporteInsertar)
        {
            List<ReporteDto> data = new List<ReporteDto>();
            try
            {
                var colaboradorNoNulos = BaseDomainHelpers.ValidarCamposNulosVacios(reporteInsertar);
                if (!colaboradorNoNulos.Success)
                {
                    return ApiResponseHelper.ErrorDto(data, Mensajes._06_Valores_Nulos + colaboradorNoNulos.Message);
                }

                var resulTransportistas = (from tran in _unitOfWork.Repository<Transportistas>().AsQueryable().AsNoTracking()
                                           where tran.transportista_id == reporteInsertar.transportista_id
                                           select tran).FirstOrDefault();
                if (resulTransportistas == null)
                {
                    return ApiResponseHelper.ErrorDto(data, Mensajes._23_Transportista_No_Encontrada);
                }
                var viajesQuery = ObtenerConsultaBaseDeViajes();
                var viajesFiltrados = FiltrarViajes(viajesQuery, reporteInsertar.fechaInicio, reporteInsertar.fechaFin, reporteInsertar.transportista_id);
                var resumenPorViaje = AgruparViajesPorEncabezado(viajesFiltrados);
                var totalGeneral = CalcularTotalGeneral(viajesQuery);

                return ApiResponseHelper.Success(CombinarResultadosYOrdenar(resumenPorViaje, totalGeneral));
            }
            catch
            {
                return ApiResponseHelper.ErrorDto(data, Mensajes._15_Error_Operacion);
            }
        }

        private IQueryable<ReporteDto> ObtenerConsultaBaseDeViajes()
        {
            try
            {
                return from enc in _context.Viajes
                       join det in _context.ViajesDetalles on enc.viaje_id equals det.viaje_id
                       join col in _context.Colaboradores on det.colaborador_id equals col.colaborador_id
                       join trans in _context.Transportistas on enc.transportista_id equals trans.transportista_id
                       join perso1 in _context.Personas on col.persona_id equals perso1.persona_id
                       join perso2 in _context.Personas on trans.persona_id equals perso2.persona_id
                       join usua in _context.Usuarios on enc.usuario_creacion equals usua.usuario_id
                       join colsu in _context.SucursalesColaboradores on col.colaborador_id equals colsu.colaborador_id
                       join suc in _context.Sucursales on colsu.sucursal_id equals suc.sucursal_id
                       select new ReporteDto
                       {
                           viajeEncabezadoId = enc.viaje_id,
                           Sucursal = suc.nombre,
                           UsuarioCreacion = usua.nombre,
                           Colaborador = ObtenerNombreCompleto(perso1),
                           Transportista = ObtenerNombreCompleto(perso2),
                           TotalAPagar = enc.total_pagar,
                           TotalDeKilometros = (double)enc.distancia_recorrida_km,
                           fechaCreacion = enc.fecha_creacion,
                           transportistaId = enc.transportista_id
                       };
            }
            catch
            {
                return Enumerable.Empty<ReporteDto>().AsQueryable();
            }
        }

        public static string ObtenerNombreCompleto(Personas persona)
        {
            return $"{persona.primer_nombre} {(persona.segundo_nombre ?? "")} {persona.primer_apellido}".Trim();
        }

        private IEnumerable<ReporteDto> FiltrarViajes(
            IQueryable<ReporteDto> query,
            DateTime? fechaInicio,
            DateTime? fechaFin,
            int? transportistaId)
        {
            try
            {
                return query.AsEnumerable()
                            .Where(x => _dominioService.FiltrarPorFechas(x.fechaCreacion, fechaInicio, fechaFin) &&
                                       _dominioService.FiltrarPorTransportista(x.transportistaId, transportistaId));
            }
            catch
            {
                return Enumerable.Empty<ReporteDto>();
            }
        }

        private static IEnumerable<ReporteDto> AgruparViajesPorEncabezado(IEnumerable<ReporteDto> viajes)
        {
            try
            {
                return viajes.GroupBy(x => new
                {
                    x.viajeEncabezadoId,
                    x.Sucursal,
                    x.UsuarioCreacion,
                    x.Colaborador,
                    x.Transportista
                })
                            .Select(g => new ReporteDto
                            {
                                viajeEncabezadoId = g.Key.viajeEncabezadoId,
                                Sucursal = g.Key.Sucursal,
                                UsuarioCreacion = g.Key.UsuarioCreacion,
                                Colaborador = g.Key.Colaborador,
                                Transportista = g.Key.Transportista,
                                TotalAPagar = g.Sum(x => x.TotalAPagar),
                                TotalDeKilometros = g.Sum(x => x.TotalDeKilometros),
                                GranTotalAPagar = null,
                                GranTotalDeKilometros = null
                            });
            }
            catch
            {
                return Enumerable.Empty<ReporteDto>();
            }
        }

        private static IEnumerable<ReporteDto> CalcularTotalGeneral(IQueryable<ReporteDto> query)
        {
            try
            {
                return query.GroupBy(x => 1)
                            .Select(g => new ReporteDto
                            {
                                viajeEncabezadoId = null,
                                Sucursal = null,
                                UsuarioCreacion = null,
                                Colaborador = null,
                                Transportista = null,
                                TotalAPagar = null,
                                TotalDeKilometros = null,
                                GranTotalAPagar = g.Sum(x => x.TotalAPagar),
                                GranTotalDeKilometros = g.Sum(x => x.TotalDeKilometros)
                            });
            }
            catch
            {
                return Enumerable.Empty<ReporteDto>();
            }
        }

        private static List<ReporteDto> CombinarResultadosYOrdenar(IEnumerable<ReporteDto> detalles, IEnumerable<ReporteDto> totales)
        {
            try
            {
                return detalles.Union(totales)
                              .OrderBy(x => x.viajeEncabezadoId)
                              .ToList();
            }
            catch
            {
                return new List<ReporteDto>();
            }
        }
    }
}
