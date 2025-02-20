namespace Academia.Translogix.WebApi.Infrastructure._ApiResponses
{

    public static class ApiResponseHelper
    {
        public static ApiResponse<T> Success<T>(T data, string message = "Operación exitosa")
        {
            var response = new ApiResponse<T>(
                success: true,
                message: message,
                data: data,
                statusCode: 200
            );
            return response;
        }

        public static ApiResponse<string> SuccessMessage(string message = "Operación exitosa")
        {
            var response = new ApiResponse<string>(
                success: true,
                message: message,
                data: null,
                statusCode: 200
            );
            return response;
        }

        public static ApiResponse<string> Error(string message)
        {
            var response = new ApiResponse<string>(
                success: false,
                message: message,
                data: null,
                statusCode: 400
            );
            return response;
        }

        public static ApiResponse<T> ErrorDto<T>(string message)
        {
            var response = new ApiResponse<T>(
                success: false,
                message: message,
                data: default(T),
                statusCode: 400
            );
            return response;
        }

        public static ApiResponse<T> NotFound<T>(string message = "No encontrado")
        {
            var response = new ApiResponse<T>(
                success: false,
                message: message,
                data: default(T),
                statusCode: 404
            );
            return response;
        }
    }
}
