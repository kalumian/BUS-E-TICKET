﻿using Core_Layer.Exceptions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BUS_E_TICKET.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;
        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger) {
            _next = next;
            _logger = logger;
        }
        public async Task Invoke(HttpContext context)
        {
            try{
                await _next(context);
            }
            catch (HttpException httpException)
            {
                await HandleHttpExceptionAsync(context, httpException);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred.");
                await HandleExceptionAsync(context, ex);
            }
        }
        private static Task HandleHttpExceptionAsync(HttpContext context, HttpException exception)
        {
            context.Response.StatusCode = exception.StatusCode;
            context.Response.ContentType = "application/json";
            var response = Utilities.ResponeHelper.GetApiRespone(
                StatusCode: Convert.ToInt16(exception.StatusCode),
                Error: exception.Message.ToString()
            );
            return context.Response.WriteAsJsonAsync(response);
        }
        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = "application/json";

            var response = new
            {
                Error = "An unexpected error occurred." + exception.Message,
                StatusCode = 500
            };

            return context.Response.WriteAsJsonAsync(response);
        }
    }
}
