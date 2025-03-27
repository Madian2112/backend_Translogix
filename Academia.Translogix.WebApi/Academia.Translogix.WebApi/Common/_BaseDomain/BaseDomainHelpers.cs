using System.Reflection;
using System.Text.Json.Serialization;
using Academia.Translogix.WebApi.Common._ApiResponses;

namespace Academia.Translogix.WebApi.Common._BaseDomain
{
    public static class BaseDomainHelpers
    {
        public static ApiResponse<T> ValidarCamposNulosVacios<T>(T dto) where T : class, new()
        {
            if (dto == null)
            {
                return new ApiResponse<T>(false, "El objeto DTO es nulo", new T(), 400);
            }

            var errorMessages = ValidarPropiedades(dto);
            return errorMessages.Count > 0
                ? new ApiResponse<T>(false, string.Join("; ", errorMessages), dto, 400)
                : new ApiResponse<T>(true, "Validación exitosa", dto, 200);
        }

        private static List<string> ValidarPropiedades<T>(T dto) where T : class
        {
            var properties = typeof(T).GetProperties();
            var errorMessages = new List<string>();

            foreach (var property in properties)
            {
                if (Attribute.IsDefined(property, typeof(JsonIgnoreAttribute)))
                    continue;

                var value = property.GetValue(dto);
                errorMessages.AddRange(ValidarPropiedad(property, value ?? ""));
            }

            return errorMessages;
        }

        private static string[] ValidarPropiedad(PropertyInfo property, object value)
        {
            if (value == null)
            {
                return new[] { $"El campo '{property.Name}' no puede ser nulo" };
            }

            bool isString = property.PropertyType == typeof(string);
            if (isString && string.IsNullOrWhiteSpace(value as string))
            {
                return new[] { $"El campo '{property.Name}' no puede estar vacío" };
            }

            bool isCollection = typeof(System.Collections.IEnumerable).IsAssignableFrom(property.PropertyType) && !isString;
            if (isCollection && EsColeccionVacia(value))
            {
                return new[] { $"El campo '{property.Name}' no puede estar vacío" };
            }

            return Array.Empty<string>();
        }

        private static bool EsColeccionVacia(object value)
        {
            var collection = value as System.Collections.IEnumerable;
            return collection != null && !collection.GetEnumerator().MoveNext();
        }

        public static ApiResponse<T> ValidadObtenerDatosNulos<T>(T dto)
        {
            return new ApiResponse<T>(
                dto != null,
                dto == null ? "Datos nulos" : "Datos no nulos",
                dto,
                404
            );
        }
    }
}