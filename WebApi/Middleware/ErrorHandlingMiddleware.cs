using Application.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;

namespace WebApi.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch(Exception ex)
            {
                await HandleExceptionAsync(ex, _logger, context);
            }
        }

        private async Task HandleExceptionAsync(Exception ex, ILogger<ErrorHandlingMiddleware> logger, HttpContext context)
        {
            object error = null;

            switch(ex)
            {
                case RestException re:
                    logger.LogError(ex, "REST Exception");
                    error = re.Errors;
                    context.Response.StatusCode = (int)re.Code;
                    break;
                case Exception e:
                    logger.LogError(ex, "SERVER ERROR");
                    error = string.IsNullOrWhiteSpace(e.Message) ? "Error" : e.Message;
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }
            
            context.Response.ContentType = "application/json";
            if(error != null)
            {
                var result = JsonConvert.SerializeObject(error);

                await context.Response.WriteAsync(result);
            }


        }
    }
}
