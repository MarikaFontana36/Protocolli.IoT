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
            Random random = new Random();

            var drone = new Drone();
            drone.Id = 0;
            drone.Date = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
            drone.Position = "Position";
            var velocita = random.NextDouble() * 20;
            drone.Speed = Math.Round(velocita, 2);
            var batteria = random.Next(100);//massimo 100
            drone.BatteryLevel = batteria;
            
            var wb = new WebClient();
            var data = JsonSerializer.Serialize(drone);
            string url = "http://localhost:44336/api/Drones";

            using (var client = new HttpClient())
            {
                var response = await client.PostAsync(url, new StringContent(data, Encoding.UTF8, "application/json"));
            }
        }
    }
}
