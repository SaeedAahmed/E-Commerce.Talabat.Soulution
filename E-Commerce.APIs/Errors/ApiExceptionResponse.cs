﻿namespace E_Commerce.APIs.Errors
{
    public class ApiExceptionResponse : ApiResponse
    {
        public string? Details { get; set; }
        public ApiExceptionResponse(int StatusCode , string? message=null , string? details=null)
            :base(StatusCode , message)
        {
            Details = details;
        }
    }
}
