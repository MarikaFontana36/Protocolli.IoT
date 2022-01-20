using ITS.DIQU.FontanaScapolan.ServerDrone.ApplicationCore.Interfaces.Data;
using ITS.DIQU.FontanaScapolan.ServerDrone.ApplicationCore.Interfaces.Services;
using ITS.DIQU.FontanaScapolan.ServerDrone.ApplicationCore.Services;
using ITS.DIQU.FontanaScapolan.ServerDrone.Infrastructure.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace ITS.DIQU.FontanaScapolan.ServerDrone.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ITS.DIQU.FontanaScapolan.ServerDrone.API", Version = "v1" });
            });
            //quando in un progetto viene richiesto un IDronesService passa DronesService
            services.AddSingleton<IDronesService, DronesService>();
            //quando in un progetto viene richiesto un IDronesRepository passa DronesRepository
            services.AddSingleton<IDronesRepository, DronesRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ITS.DIQU.FontanaScapolan.ServerDrone.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
