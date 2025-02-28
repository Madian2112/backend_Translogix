using Academia.Translogix.WebApi._Features.Viaj.Dtos;
using Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase.Entities.Gral;
using Azure;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using static Academia.Translogix.WebApi._Features.Viaj.Dtos.ViajesDto;
using static Academia.Translogix.WebApi._Features.Viaj.Services.ViajeService;

namespace Academia.Translogix.WebApi._Features.Gral.Services
{
    public class _OpenRouteService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey = "5b3ce3597851110001cf6248a86cdb0e13774ed292ad0fe1ed7e9640"; 

        public _OpenRouteService()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://api.openrouteservice.org/")
            };
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);
        }


        public async Task<AgruparRutasResponse> AgruparRutasPorDistanciaAsync(
        double[] origen,
        List<UbicacionesViaje> ubicaciones,
        List<Transportista> transportistas)
        {
            if (ubicaciones == null || !ubicaciones.Any())
                throw new ArgumentException("Se requiere al menos una ubicación.");

            var todasLasCoordenadas = new List<double[]> { origen };
            todasLasCoordenadas.AddRange(ubicaciones.Select(u => u.Ubicaciones));

            var matrizDistancias = await ObtenerMatrizDistanciasAsync(todasLasCoordenadas);

            var ubicacionesOrdenadas = ubicaciones
                .Select((u, indice) => new { Ubicacion = u, DistanciaDesdeOrigen = matrizDistancias[0][indice + 1] })
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
                    if (grupoActual.Any())
                        grupos.Add(grupoActual);

                    grupoActual = new List<UbicacionesViaje> { ubicacion };
                    distanciaAcumulada = ubicacion.DistanciaKm;
                }
            }

            if (grupoActual.Any())
                grupos.Add(grupoActual);

            var rutasAgrupadas = new List<RutaAgrupadaResponse>();
            int x = 0;
            int totalGrupos = grupos.Count;
            int gruposProcesados = 0;

            foreach (var grupo in grupos)
            {
                if (x >= transportistas.Count)
                    break;

                var coordenadasGrupo = new List<double[]> { origen };
                coordenadasGrupo.AddRange(grupo.Select(g => g.Ubicaciones));

                var indiceUltimaUbicacion = todasLasCoordenadas.IndexOf(coordenadasGrupo.Last());
                var distanciaMaximaDesdeOrigen = matrizDistancias[0][indiceUltimaUbicacion];

                var jsonRuta = await ObtenerRutaAsync(coordenadasGrupo);
                var infoRuta = JsonConvert.DeserializeObject<RouteResponse>(jsonRuta);

                var informacionRuta = new RutaAgrupadaResponse
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
                        TotalPagoPorColaborador = transportistas[x].precio_km * g.DistanciaKm,
                    }).ToList(),
                    transportista_id = transportistas[x]?.transportista_id ?? 0
                };

                informacionRuta.TotalPagoPorViaje = informacionRuta.Colaboradores.Sum(c => c.TotalPagoPorColaborador);

                rutasAgrupadas.Add(informacionRuta);
                gruposProcesados++;
                x++;
            }

            string mensaje = string.Empty;
            int gruposNoProcesados = totalGrupos - gruposProcesados;
            if (gruposNoProcesados > 0)
            {
                mensaje = $"Se crearon y procesaron {gruposProcesados} de {totalGrupos} viajes. " +
                          $"{gruposNoProcesados} viaje(s) no se procesaron debido a que no hay suficientes transportistas disponibles. Cree un nuevo viaje";
            }
            else
            {
                mensaje = $"Se crearon y procesaron correctamente todos los {totalGrupos} viajes.";
            }

            return new AgruparRutasResponse
            {
                RutasAgrupadas = rutasAgrupadas,
                Mensaje = mensaje
            };
        }

        private async Task<double[][]> ObtenerMatrizDistanciasAsync(List<double[]> coordenadas)
        {
            var cuerpoSolicitud = new { locations = coordenadas, metrics = new[] { "distance" }, units = "km" };
            var contenido = new StringContent(JsonConvert.SerializeObject(cuerpoSolicitud), Encoding.UTF8, "application/json");
            var respuesta = await _httpClient.PostAsync("v2/matrix/driving-car", contenido);

            if (!respuesta.IsSuccessStatusCode)
            {
                var contenidoError = await respuesta.Content.ReadAsStringAsync();
                throw new Exception($"Error {respuesta.StatusCode}: {respuesta.ReasonPhrase}. Detalles: {contenidoError}");
            }

            var jsonRespuesta = await respuesta.Content.ReadAsStringAsync();
            var datosMatriz = JsonConvert.DeserializeObject<dynamic>(jsonRespuesta);
            return JsonConvert.DeserializeObject<double[][]>(datosMatriz.distances.ToString());
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
                var contenidoError = await respuesta.Content.ReadAsStringAsync();
                throw new Exception($"Error {respuesta.StatusCode}: {respuesta.ReasonPhrase}. Detalles: {contenidoError}");
            }

            return await respuesta.Content.ReadAsStringAsync();
        }
    }
}
