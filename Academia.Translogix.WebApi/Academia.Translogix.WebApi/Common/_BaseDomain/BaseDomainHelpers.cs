using System.Text.Json.Serialization;
using Academia.Translogix.WebApi.Common._ApiResponses;

namespace Academia.Translogix.WebApi.Common._BaseDomain
{
    public static class BaseDomainHelpers
    {
        public static ApiResponse<T> ValidarCamposNulosVacios<T>(T dto) where T : class
        {
            if (dto == null)
            {
                return new ApiResponse<T>(
                    success: false,
                    message: "El objeto DTO es nulo",
                    data: null,
                    statusCode: 400
                );
            }

            var properties = typeof(T).GetProperties();
            var errorMessages = new List<string>(); 

            foreach (var property in properties)
            {
                if (Attribute.IsDefined(property, typeof(JsonIgnoreAttribute)))
                {
                    continue;
                }

                var value = property.GetValue(dto);

                if (value == null)
                {
                    errorMessages.Add($"El campo '{property.Name}' no puede ser nulo");
                    continue; 
                }

                if (property.PropertyType == typeof(string))
                {
                    string stringValue = value as string;
                    if (string.IsNullOrWhiteSpace(stringValue))
                    {
                        errorMessages.Add($"El campo '{property.Name}' no puede estar vacío");
                    }
                }

                if (typeof(System.Collections.IEnumerable).IsAssignableFrom(property.PropertyType) &&
                    property.PropertyType != typeof(string))
                {
                    var collection = value as System.Collections.IEnumerable;
                    if (collection != null && !collection.GetEnumerator().MoveNext())
                    {
                        errorMessages.Add($"El campo '{property.Name}' no puede estar vacío");
                    }
                }
            }

            if (errorMessages.Count > 0)
            {
                string combinedMessage = string.Join("; ", errorMessages); 
                return new ApiResponse<T>(
                    success: false,
                    message: combinedMessage,
                    data: dto,
                    statusCode: 400
                );
            }

            return new ApiResponse<T>(
                success: true,
                message: "Validación exitosa",
                data: dto,
                statusCode: 200
            );
        }
    }
}
