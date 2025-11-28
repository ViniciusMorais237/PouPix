using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using API.Entities.Exception;

namespace API.Middleware
{
    public class ExceptionMiddleware
    {
        public ExceptionMiddleware(RequestDelegate request, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
        {
            _request = request;
            _logger = logger;
            _env = env;
        }
        private readonly RequestDelegate _request;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _env;

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _request(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = ex switch
                {
                    InvalidDataException => StatusCodes.Status422UnprocessableEntity,
                    _ => StatusCodes.Status400BadRequest
                } ;

                var response = _env.IsDevelopment() ?
                    new ApiException(context.Response.StatusCode.ToString(), ex.Message, ex.StackTrace!.ToString()) :
                    new ApiException(context.Response.StatusCode.ToString(), ex.Message);

                var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

                var json = JsonSerializer.Serialize(response, options);
                await context.Response.WriteAsync(json);
            }
        }
    }
}