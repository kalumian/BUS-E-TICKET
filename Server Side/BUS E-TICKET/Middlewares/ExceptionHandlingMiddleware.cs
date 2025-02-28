using Core_Layer.Exceptions;
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

            // التحقق من وجود InnerException قبل الوصول إليها
            var innerExceptionMessage = exception.InnerException?.Message ?? "";

            // إنشاء استجابة تحتوي على رسالة الخطأ
            var response = Utilities.ResponeHelper.GetApiRespone(
                StatusCode: Convert.ToInt16(exception.StatusCode),
                Error: exception.Message.ToString() + " " + innerExceptionMessage
            );

            return context.Response.WriteAsJsonAsync(response);
        }
        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = "application/json";
            var innerExceptionMessage = exception.InnerException?.Message ?? "";

            var response = new
            {
                Error = "An unexpected error occurred." + exception.Message + " " + innerExceptionMessage,
                StatusCode = 500
            };

            return context.Response.WriteAsJsonAsync(response);
        }
    }
}
