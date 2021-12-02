using ITS.DIQU.FontanaScapolan.ServerDrone.ApplicationCore.Interfaces.Data;
using ITS.DIQU.FontanaScapolan.ServerDrone.ApplicationCore.Interfaces.Services;
using ITS.DIQU.FontanaScapolan.ServerDrone.ApplicationCore.Services;
using ITS.DIQU.FontanaScapolan.ServerDrone.Infrastructure.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITS.DIQU.FontanaScapolan.ServerDrone.MQTTClient
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    //quando in un progetto viene richiesto un IDronesService passa DronesService
                    services.AddSingleton<IDronesService, DronesService>();
                    //quando in un progetto viene richiesto un IDronesRepository passa DronesRepository
                    services.AddSingleton<IDronesRepository, DronesRepository>();
                    services.AddHostedService<Worker>();
                });
    }
}
