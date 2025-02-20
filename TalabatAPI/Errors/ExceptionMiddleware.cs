using Azure.Core;
using System.Buffers;
using System.Net;
using System.Text.Json;

namespace TalabatAPI.Errors
{
    //ByConvention
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IWebHostEnvironment _env;

        public ExceptionMiddleware(RequestDelegate next,ILogger<ExceptionMiddleware> logger,IWebHostEnvironment env)
        {
            _next = next;
           _logger = logger;
            _env = env;
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next.Invoke(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                httpContext.Response.ContentType = "application/json";
                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var response =
                    _env.IsDevelopment ()?
                    new ApiExtentionResponse((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace.ToString())
                    : new ApiExtentionResponse((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace.ToString());
                var option = new JsonSerializerOptions()
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                };
                var json = JsonSerializer.Serialize(response, option);
               await httpContext.Response.WriteAsync(json);
            }

        }
    }
}
