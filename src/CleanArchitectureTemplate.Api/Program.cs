#if (shared && serilog)
using CleanArchitectureTemplate.Shared.Logging;
#endif
#if (!shared && serilog)
using CleanArchitectureTemplate.Infrastructure.Logging;
#endif
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace CleanArchitectureTemplate.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        #if (serilog)
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .UseLogging();
        #else
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
        #endif
    }
}
