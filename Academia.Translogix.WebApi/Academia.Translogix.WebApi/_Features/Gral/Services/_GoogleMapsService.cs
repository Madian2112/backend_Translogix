using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Academia.Translogix.WebApi.Infrastructure;
using Academia.Translogix.WebApi.Infrastructure.TranslogixDataBase.Entities.Gral;
using Farsiman.Domain.Core.Standard.Repositories;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
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
            _apiKey = "AIzaSyAhGb5-x69f-4wZABaurBrprU6PHRgXRKk";
        }

        public class Location
        {
            public decimal Lat { get; set; }
            public decimal Lng { get; set; }
        }

        public class LocationInfo
        {
            public bool Success { get; set; }
            public string MensajeError { get; set; }
            public string DireccionFormateada { get; set; }
        }

        public LocationInfo GetLocationInfoAsync(Location location)
        {
            try
            {
                string latStr = location.Lat.ToString(System.Globalization.CultureInfo.InvariantCulture);
                string lngStr = location.Lng.ToString(System.Globalization.CultureInfo.InvariantCulture);

                string url = $"https://maps.googleapis.com/maps/api/geocode/json?latlng={latStr},{lngStr}&key={_apiKey}";

                var response =  _httpClient.GetAsync(url).Result;
                response.EnsureSuccessStatusCode(); 

                string responseBody =  response.Content.ReadAsStringAsync().Result;
                JObject jsonResponse = JObject.Parse(responseBody);

                string status = jsonResponse["status"].ToString();
                JArray results = (JArray)jsonResponse["results"];

                if (status != "OK" || results.Count == 0)
                {
                    return new LocationInfo
                    {
                        Success = false,
                        MensajeError = $"No se encontraron resultados para las coordenadas: {location.Lat}, {location.Lng}"
                    };
                }

                string ciudad = (string)results[2]["address_components"][0]["long_name"];
                string departamento = (string)results[2]["address_components"][0]["long_name"];

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