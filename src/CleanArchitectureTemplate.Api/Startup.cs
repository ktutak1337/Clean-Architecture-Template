using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using CleanArchitectureTemplate.Application;
using CleanArchitectureTemplate.Infrastructure;
using Microsoft.AspNetCore.Http;

namespace CleanArchitectureTemplate.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
            => Configuration = configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddApplication();
            services.AddInfrastructure();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseInfrastructure();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/",  context => context.Response.WriteAsync("Hello Mario, the princess is in another castle!"));
                endpoints.MapControllers();
            });
        }
    }
}
