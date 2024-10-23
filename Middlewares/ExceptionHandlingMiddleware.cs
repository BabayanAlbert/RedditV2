using System.Text.Json;

namespace Reddit.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await this._next(context);
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, "For Developer: Unexpected error occurred on the server!");

                var errorResponseModel = new ErrorResponse
                {
                    Message = "Unexpected error occurred on the server!",
                    Description = ex.Message
                };

                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType = "application/json";

                var jsonResponse = JsonSerializer.Serialize(errorResponseModel);

                await context.Response.WriteAsync(jsonResponse);
            }
        }

    }
}