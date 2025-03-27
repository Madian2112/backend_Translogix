﻿namespace Academia.Translogix.WebApi.Common._ApiResponses
{

    public static class ApiResponseHelper
    {
        public static ApiResponse<T> Success<T>(T data, string message = "Operación exitosa", int statusCode = 200)
        {
            var response = new ApiResponse<T>(
                success: true,
                message: message,
                data: data,
                statusCode: statusCode
            );
            return response;
        }

        public static ApiResponse<string> SuccessMessage(string message = "Operación exitosa")
        {
            var response = new ApiResponse<string>(
                success: true,
                message: message,
                data: string.Empty,
                statusCode: 200
            );
            return response;
        }

        public static ApiResponse<T> ErrorList<T>(T data, string message = "Operación exitosa")
        {
            var response = new ApiResponse<T>(
                success: true,
                message: message,
                data: data,
                statusCode: 400
            );
            return response;
        }

        public static ApiResponse<string> Error(string message)
        {
            var response = new ApiResponse<string>(
                success: false,
                message: message,
                data: string.Empty,
                statusCode: 400
            );
            return response;
        }

        public static ApiResponse<T> ErrorDto<T>(T data, string message)
        {
            var response = new ApiResponse<T>(
                success: false,
                message: message,
                data: data,
                statusCode: 400
            );
            return response;
        }

        public static ApiResponse<T> NotFound<T>(T data, string message = "No encontrado")
        {
            var response = new ApiResponse<T>(
                success: false,
                message: message,
                data: data,
                statusCode: 404
            );
            return response;
        }

        public static ApiResponse<T> Unauthorized<T>(T data, string message = "No encontrado")
        {
            var response = new ApiResponse<T>(
                success: false,
                message: message,
                data: data,
                statusCode: 401
            );
            return response;
        }
    }
}
