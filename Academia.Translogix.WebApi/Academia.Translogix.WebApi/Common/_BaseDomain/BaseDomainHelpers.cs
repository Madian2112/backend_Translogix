using Academia.Translogix.WebApi.Common._ApiResponses;

namespace Academia.Translogix.WebApi.Common._BaseDomain
{
    public static class BaseDomainHelpers
    {
        public static ApiResponse<T> ValidarCamposNulos<T>(T dto) where T : class
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
            foreach (var property in properties)
            {
                var value = property.GetValue(dto);

                // Validar propiedades según su tipo
                if (value == null)
                {
                    return new ApiResponse<T>(
                        success: false,
                        message: $"El campo {property.Name} no puede ser nulo",
                        data: dto,
                        statusCode: 400
                    );
                }

                // Validar strings vacíos
                if (property.PropertyType == typeof(string))
                {
                    string stringValue = value as string;
                    if (string.IsNullOrWhiteSpace(stringValue))
                    {
                        return new ApiResponse<T>(
                            success: false,
                            message: $"El campo {property.Name} no puede estar vacío",
                            data: dto,
                            statusCode: 400
                        );
                    }
                }

                // Validar colecciones vacías (si aplica)
                if (typeof(System.Collections.IEnumerable).IsAssignableFrom(property.PropertyType) &&
                    property.PropertyType != typeof(string))
                {
                    var collection = value as System.Collections.IEnumerable;
                    if (collection != null && !collection.GetEnumerator().MoveNext())
                    {
                        return new ApiResponse<T>(
                            success: false,
                            message: $"El campo {property.Name} no puede estar vacío",
                            data: dto,
                            statusCode: 400
                        );
                    }
                }
            }

            // Si todo está bien
            return new ApiResponse<T>(
                success: true,
                message: "Validación exitosa",
                data: dto,
                statusCode: 200
            );
        }
    }
}
