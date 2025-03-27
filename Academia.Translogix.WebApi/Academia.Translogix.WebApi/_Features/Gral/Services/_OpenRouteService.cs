using System.Net.Http.Headers;
using System.Text;
using Academia.Translogix.WebApi._Features.Viaj.Dtos;
using Newtonsoft.Json;

namespace Academia.Translogix.WebApi._Features.Gral.Services
{
    public class OpenRouteServicer
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey = "5b3ce3597851110001cf6248a86cdb0e13774ed292ad0fe1ed7e9640";
        public OpenRouteServicer(IConfiguration configuration)
        {
            string _url = configuration.GetValue<string>("OpenRouteService:BaseUrl") ?? "";

            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(_url)
            };
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);
        }


        public async Task<AgruparRutasResponse> AgruparRutasPorDistanciaAsync(
    double[] origen,
    List<UbicacionesViaje> ubicaciones,
    List<Transportista> transportistas)
        {
            var todasLasCoordenadas = new List<double[]> { origen };
            todasLasCoordenadas.AddRange(ubicaciones.Select(u => u.Ubicaciones));
            var matrizDistancias = await ObtenerMatrizDistanciasAsync(todasLasCoordenadas);

            var ubicacionesOrdenadas = ubicaciones
                .Select((u, indice) => new { Ubicacion = u, DistanciaDesdeOrigen = matrizDistancias?[0][indice + 1] })
                .OrderBy(x => x.DistanciaDesdeOrigen)
                .Select(x => x.Ubicacion)
                .ToList();

            var grupos = new List<List<UbicacionesViaje>>();
            var grupoActual = new List<UbicacionesViaje>();
            decimal distanciaAcumulada = 0;

            foreach (var ubicacion in ubicacionesOrdenadas)
            {
                if (distanciaAcumulada + ubicacion.DistanciaKm <= 100)
                {
                    grupoActual.Add(ubicacion);
                    distanciaAcumulada += ubicacion.DistanciaKm;
                }
                else
                {
                    if (grupoActual.Count <= 0)
                        grupos.Add(grupoActual);

                    grupoActual = new List<UbicacionesViaje> { ubicacion };
                    distanciaAcumulada = ubicacion.DistanciaKm;
                }
            }
            if (grupoActual.Count <= 0)
                grupos.Add(grupoActual);

            var rutasAgrupadas = await ProcesarGruposEnRutas(grupos, transportistas, todasLasCoordenadas, matrizDistancias ?? Array.Empty<double[]>(), origen);

            int gruposProcesados = rutasAgrupadas.Count;
            int totalGrupos = grupos.Count;
            int gruposNoProcesados = totalGrupos - gruposProcesados;
            string mensaje = gruposNoProcesados > 0
                ? $"Se crearon y procesaron {gruposProcesados} de {totalGrupos} viajes. " +
                  $"{gruposNoProcesados} viaje(s) no se procesaron debido a que no hay suficientes transportistas disponibles. Cree un nuevo viaje"
                : $"Se crearon y procesaron correctamente todos los {totalGrupos} viajes.";

            return new AgruparRutasResponse
            {
                RutasAgrupadas = rutasAgrupadas,
                Mensaje = mensaje
            };
        }

        private async Task<List<RutaAgrupadaResponse>> ProcesarGruposEnRutas(
            List<List<UbicacionesViaje>> grupos,
            List<Transportista> transportistas,
            List<double[]> todasLasCoordenadas,
            double[][] matrizDistancias,
            double[] origen)
        {
            var rutasAgrupadas = new List<RutaAgrupadaResponse>();
            int indiceTransportista = 0;

            foreach (var grupo in grupos)
            {
                if (indiceTransportista >= transportistas.Count)
                    break;

                var coordenadasGrupo = new List<double[]> { origen };
                coordenadasGrupo.AddRange(grupo.Select(g => g.Ubicaciones));

                var indiceUltimaUbicacion = todasLasCoordenadas.IndexOf(coordenadasGrupo.Last());
                var distanciaMaximaDesdeOrigen = matrizDistancias[0][indiceUltimaUbicacion];

                var jsonRuta = await ObtenerRutaAsync(coordenadasGrupo);
                var infoRuta = JsonConvert.DeserializeObject<RouteResponse>(jsonRuta);

                if (infoRuta?.Routes == null || infoRuta.Routes.Count <= 0)
                    return rutasAgrupadas;

                var rutaAgrupada = new RutaAgrupadaResponse
                {
                    Distance = infoRuta.Routes[0].Summary.Distance,
                    Duration = infoRuta.Routes[0].Summary.Duration,
                    Waypoints = infoRuta.Routes[0].Waypoints,
                    Steps = infoRuta.Routes[0].Segments.Select(s => s.Steps.ToList()).ToList(),
                    DistanciaMaximaDesdeOrigen = distanciaMaximaDesdeOrigen,
                    DistanciaAcumuladaKm = grupo.Sum(g => g.DistanciaKm),
                    Colaboradores = grupo.Select(g => new ColaboradorInfo
                    {
                        ColaboradorId = g.colaborador_id,
                        DistanciaKm = g.DistanciaKm,
                        TotalPagoPorColaborador = transportistas[indiceTransportista].precio_km * g.DistanciaKm,
                    }).ToList(),
                    transportista_id = transportistas[indiceTransportista].transportista_id
                };
                rutaAgrupada.TotalPagoPorViaje = rutaAgrupada.Colaboradores.Sum(c => c.TotalPagoPorColaborador);

                rutasAgrupadas.Add(rutaAgrupada);
                indiceTransportista++;
            }

            return rutasAgrupadas;
        }


        private async Task<double[][]?> ObtenerMatrizDistanciasAsync(List<double[]> coordenadas)
        {
            var cuerpoSolicitud = new { locations = coordenadas, metrics = new[] { "distance" }, units = "km" };
            var contenido = new StringContent(JsonConvert.SerializeObject(cuerpoSolicitud), Encoding.UTF8, "application/json");
            var respuesta = await _httpClient.PostAsync("v2/matrix/driving-car", contenido);

            if (!respuesta.IsSuccessStatusCode)
            {
                return null;
            }

            var jsonRespuesta = await respuesta.Content.ReadAsStringAsync();
            var datosMatriz = JsonConvert.DeserializeObject<dynamic>(jsonRespuesta);
            return datosMatriz?.distances != null ? JsonConvert.DeserializeObject<double[][]>(datosMatriz.distances.ToString()) : null;
        }

        private async Task<string> ObtenerRutaAsync(List<double[]> coordenadas)
        {
            var cuerpoSolicitud = new
            {
                coordinates = coordenadas,
                profile = "driving-car",
                format = "json",
                geometry = true,
                instructions = true,
                units = "km"
            };

            var contenido = new StringContent(JsonConvert.SerializeObject(cuerpoSolicitud), Encoding.UTF8, "application/json");
            var respuesta = await _httpClient.PostAsync("v2/directions/driving-car", contenido);

            if (!respuesta.IsSuccessStatusCode)
            {
                return await respuesta.Content.ReadAsStringAsync();
            }

            return await respuesta.Content.ReadAsStringAsync();
        }
    }
}
