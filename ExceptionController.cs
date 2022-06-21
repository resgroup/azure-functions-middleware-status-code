using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;

namespace MiddlewareStatusCode
{
    public class ExceptionController
    {
        [Function("ThrowException")]
        public void ThrowException(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "throw-exception")] HttpRequestData req)
            => throw new System.Exception("Test message");
    }
}
