using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.Functions.Worker.Middleware;
using Microsoft.Extensions.Logging;

namespace MiddlewareStatusCode
{
    public class CustomExceptionHandlerMiddleware : IFunctionsWorkerMiddleware
    {
        private readonly ILogger<CustomExceptionHandlerMiddleware> _logger;

        public CustomExceptionHandlerMiddleware(ILogger<CustomExceptionHandlerMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task Invoke(FunctionContext context, FunctionExecutionDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception handler middleware caught exception {ex}", ex);
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(FunctionContext context, Exception exception)
        {
            var request = await context.GetHttpRequestDataAsync();

            var response = request.CreateResponse(HttpStatusCode.NotFound);

            await response.WriteAsJsonAsync(new { exception.Message }, response.StatusCode);

            context.GetInvocationResult().Value = response;
        }
    }
}
