using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;

namespace MiddlewareStatusCode
{
    public class ExceptionController
    {
        public class MyResponseType
        {
            public string Name { get; } = "hello";
            public string Message { get; } = "World";
        }
		
        [Function("ThrowException")]
        public void ThrowException(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "throw-exception")] HttpRequestData req)
            => throw new System.Exception("Test message");

        [Function("ThrowExceptionAndReturn")]
        public MyResponseType ThrowExceptionAndReturn(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "throw-exception-and-return/{returnUsefullData}")] HttpRequestData req,
            bool returnUsefullData)
            => returnUsefullData
             ? throw new System.Exception("Test message")
             : new MyResponseType();
    }
}
