using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Protocolli.IoT.Fontana_Scapolan.Worker.Models;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Protocolli.IoT.Fontana_Scapolan.Worker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            
            while (true)
            {
                var wb = new WebClient();
                
                string url = "https://192.168.104.86:5001/api/Drones";
                var sensor = new VirtualSensor();
                string data = sensor.getJson();
                var httpClientHandler = new HttpClientHandler();
                httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, sslPolicyErrors) =>
                {
                    return true;
                };
                using (var client = new HttpClient(httpClientHandler))
                {
                    var response = await client.PostAsync(url, new StringContent(data, Encoding.UTF8, "application/json"));
                }
                Console.WriteLine(data);
                System.Threading.Thread.Sleep(20000);
            }
        }
    }
}
