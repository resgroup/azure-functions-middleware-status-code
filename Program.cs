using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace MiddlewareStatusCode
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var host = new HostBuilder()
                .ConfigureFunctionsWorkerDefaults(worker =>
                    worker
                    .UseMiddleware<CustomMiddleware.ExceptionHandlingMiddleware>()
                )
                .Build();

            await host.RunAsync();
        }
    }
}