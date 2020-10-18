using CleanArchitectureTemplate.Application;
using CleanArchitectureTemplate.Infrastructure;
#if (shared)
using CleanArchitectureTemplate.Shared;
#endif
using Convey;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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
            #if (swagger || postgres)
            services.AddInfrastructure();
            #endif
            #if (shared)
            services.AddShared();
            #endif
            services
                .AddConvey()
                .AddApplication()
                .AddInfrastructure();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseErrorHandler();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            #if (swagger)
            app.UseInfrastructure();
            #endif
        }
    }
}
