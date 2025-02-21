using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Academia.Translogix.WebApi._Features.Gral.Services
{
    public class GoogleMapsService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        
        public GoogleMapsService(IConfiguration configuration)
        {
            _httpClient = new HttpClient();
            _apiKey = "AIzaSyAhGb5-x69f-4wZABaurBrprU6PHRgXRKk";
        }

        public class Location
        {
            public decimal Lat { get; set; }
            public decimal Lng { get; set; }
        }

        //public async Task<double> CalcularDistanciaAsync(Location origen, Location destino)
        //{
        //    var url = $"https://maps.googleapis.com/maps/api/distancematrix/json" +
        //            $"?origins={origen.Lat},{origen.Lng}" +
        //            $"&destinations={destino.Lat},{destino.Lng}" +
        //            $"&mode=DRIVING" +
        //            $"&units=metric" +
        //            $"&key={_apiKey}";

        //    try
        //    {
        //        var response = await _httpClient.GetAsync(url);
        //        response.EnsureSuccessStatusCode();

        //        var content = await response.Content.ReadAsStringAsync();
        //        using JsonDocument document = JsonDocument.Parse(content);

        //        Console.WriteLine("Esta es la respuesta de la calculo desde el servicio: " + document);

        //        var root = document.RootElement;
        //        var rows = root.GetProperty("rows");
        //        var elements = rows[0].GetProperty("elements");
        //        var distance = elements[0].GetProperty("distance");
        //        var distanceInMeters = distance.GetProperty("value").GetInt32();

        //        return distanceInMeters / 1000.0; // Convertir a kilómetros
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception($"Error al calcular la distancia: {ex.Message}");
        //    }
        //}

        public async Task<string> CalcularDistanciaAsync(Location origen, Location destino)
        {
            // Asegurarse de usar el formato correcto para los números decimales (punto en lugar de coma)
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

                // Verificar el status general de la respuesta
                var status = root.GetProperty("status").GetString();
                if (status != "OK")
                {
                    throw new Exception($"Error en la respuesta de Google Maps: {status}");
                }

                var rows = root.GetProperty("rows");
                if (rows.GetArrayLength() == 0)
                {
                    throw new Exception("No se encontraron resultados");
                }

                var elements = rows[0].GetProperty("elements");
                if (elements.GetArrayLength() == 0)
                {
                    throw new Exception("No se encontraron elementos en la respuesta");
                }

                var element = elements[0];
                var elementStatus = element.GetProperty("status").GetString();
                if (elementStatus != "OK")
                {
                    throw new Exception($"Error en el elemento de la respuesta: {elementStatus}");
                }

                var distance = element.GetProperty("distance");
                var distanceInMeters = distance.GetProperty("text").ToString();

                return distanceInMeters; // Convertir a kilómetros
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al calcular la distancia: {ex.Message}");
            }
        }
    }
}