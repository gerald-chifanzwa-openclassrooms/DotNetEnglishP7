using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace WebApi.Middleware
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public RequestLoggingMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next;
            _logger = loggerFactory.CreateLogger<RequestLoggingMiddleware>();
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                var request = context.Request;
                var requestSummary = new
                {
                    Id = context.TraceIdentifier,
                    request.Path,
                    request.Method,
                    request.QueryString,
                    request.ContentType,
                    request.ContentLength,
                    Headers = request.Headers.Keys
                };
                _logger.LogInformation("Incoming request {@RequestSummary}", requestSummary);
                await _next(context);
            }
            finally
            {
                var response = context.Response;
                var responseSummary = new
                {
                    Id = context.TraceIdentifier,
                    response.StatusCode,
                    response.ContentType,
                    response.ContentLength,
                    Headers = response.Headers.Keys,
                };
                _logger.LogInformation("Outgoing Response {@ResponseSummary}", responseSummary);
            }
        }
    }
}
