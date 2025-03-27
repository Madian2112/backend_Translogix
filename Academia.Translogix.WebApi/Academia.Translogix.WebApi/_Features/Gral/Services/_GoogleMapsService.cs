using System.Globalization;
using System.Text.Json;
using Newtonsoft.Json.Linq;

namespace Academia.Translogix.WebApi._Features.Gral.Services
{
    public class GoogleMapsService
    {

        public interface IGoogleMapsService
        {
            LocationInfo GetLocationInfoAsync(Location location);
            Task<string> CalcularDistanciaAsync(Location origen, Location destino);
        }


        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public GoogleMapsService()
        {
            _httpClient = new HttpClient();
            _apiKey = "AIzaSyBEepHsu6eCFpfdeIzNi6aOD4u_sM2du64";
        }

        public class Location
        {
            public decimal Lat { get; set; }
            public decimal Lng { get; set; }
        }

        public class LocationInfo
        {
            public bool Success { get; set; }
            public string MensajeError { get; set; } = string.Empty;
            public string DireccionFormateada { get; set; } = string.Empty;
        }

        public LocationInfo GetLocationInfoAsync(Location location)
        {
            try
            {
                string latStr = location.Lat.ToString(System.Globalization.CultureInfo.InvariantCulture);
                string lngStr = location.Lng.ToString(System.Globalization.CultureInfo.InvariantCulture);

                string url = $"https://maps.googleapis.com/maps/api/geocode/json?latlng={latStr},{lngStr}&key={_apiKey}";

                var response = _httpClient.GetAsync(url).Result;
                response.EnsureSuccessStatusCode();

                string responseBody = response.Content.ReadAsStringAsync().Result;
                JObject jsonResponse = JObject.Parse(responseBody);

                string status = jsonResponse["status"]?.ToString() ?? string.Empty;

                JArray? results = (JArray?)jsonResponse["results"];

                if (status != "OK" || results == null || results.Count == 0)
                {
                    return new LocationInfo
                    {
                        Success = false,
                        MensajeError = $"No se encontraron resultados para las coordenadas: {location.Lat}, {location.Lng}"
                    };
                }

                var addressComponents = results[2]?["address_components"] as JArray;
                if (addressComponents == null || addressComponents.Count == 0)
                {
                    return new LocationInfo
                    {
                        Success = false,
                        MensajeError = "No se encontraron componentes de dirección."
                    };
                }

                string ciudad = addressComponents[0]?["long_name"]?.ToString() ?? string.Empty;

                return new LocationInfo
                {
                    Success = true,
                    DireccionFormateada = ciudad
                };
            }
            catch (Exception ex)
            {
                return new LocationInfo
                {
                    Success = false,
                    MensajeError = $"Error al obtener información de ubicación: {ex.Message}"
                };
            }
        }

        public async Task<string> CalcularDistanciaAsync(Location origen, Location destino)
        {
            var origenStr = $"{origen.Lat.ToString(CultureInfo.InvariantCulture)},{origen.Lng.ToString(CultureInfo.InvariantCulture)}";
            var destinoStr = $"{destino.Lat.ToString(CultureInfo.InvariantCulture)},{destino.Lng.ToString(CultureInfo.InvariantCulture)}";

            var url = $"https://maps.googleapis.com/maps/api/distancematrix/json" +
                    $"?origins={origenStr}" +
                    $"&destinations={destinoStr}" +
                    $"&mode=driving" +
                    $"&units=metric" +
                    $"&key={_apiKey}";

            try
            {
                var response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();
                using JsonDocument document = JsonDocument.Parse(content);

                var root = document.RootElement;

                var status = root.GetProperty("status").GetString();

                if (status != "OK")
                {
                    return "";
                }

                var rows = root.GetProperty("rows");
                if (rows.GetArrayLength() == 0)
                {
                    return "";
                }

                var elements = rows[0].GetProperty("elements");
                if (elements.GetArrayLength() == 0)
                {
                    return "";
                }

                var element = elements[0];
                var elementStatus = element.GetProperty("status").GetString();
                if (elementStatus != "OK")
                {
                    return "Error en el elemento de la respuesta:";
                }

                var distance = element.GetProperty("distance");
                var distanceInMeters = distance.GetProperty("text").ToString();

                return distanceInMeters;
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }
    }
}