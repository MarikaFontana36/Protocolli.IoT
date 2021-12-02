using ITS.DIQU.FontanaScapolan.ServerDrone.ApplicationCore.Entities;
using ITS.DIQU.FontanaScapolan.ServerDrone.ApplicationCore.Interfaces.Services;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Options;
using MQTTnet.Client.Subscribing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace ITS.DIQU.FontanaScapolan.ServerDrone.MQTTClient
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IDronesService _dronesService;
        public Worker(ILogger<Worker> logger, IDronesService dronesService)
        {
            _logger = logger;
            _dronesService = dronesService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            // Create a new MQTT client.
            var factory = new MqttFactory();
            var mqttClient = factory.CreateMqttClient();
            // Creates a new client
            var options = new MqttClientOptionsBuilder()
                                            .WithTcpServer("localhost", 1883)
                                            .Build();

            await mqttClient.ConnectAsync(options, CancellationToken.None); // Since 3.0.5 with CancellationToken

            mqttClient.UseDisconnectedHandler(async e =>
            {
                Console.WriteLine("### DISCONNECTED FROM SERVER ###");
                await Task.Delay(TimeSpan.FromSeconds(5));

                try
                {
                    await mqttClient.ConnectAsync(options, CancellationToken.None); // Since 3.0.5 with CancellationToken
                }
                catch
                {
                    Console.WriteLine("### RECONNECTING FAILED ###");
                }
            });

            mqttClient.UseConnectedHandler(async e =>
            {
                Console.WriteLine("### CONNECTED WITH SERVER ###");

                // Subscribe to a topic
                await mqttClient.SubscribeAsync(new MqttClientSubscribeOptionsBuilder().WithTopicFilter("protocolliIot/+/stato").Build());

                Console.WriteLine("### SUBSCRIBED ###");
            });

            mqttClient.UseApplicationMessageReceivedHandler(e =>
            {
                Console.WriteLine("### RECEIVED APPLICATION MESSAGE ###");
                Console.WriteLine($"+ Topic = {e.ApplicationMessage.Topic}");
                Console.WriteLine($"+ Payload = {Encoding.UTF8.GetString(e.ApplicationMessage.Payload)}");
                var drone = new Drone();
                drone = JsonSerializer.Deserialize<Drone>(e.ApplicationMessage.Payload);
                _dronesService.Insert(drone);
                Console.WriteLine($"+ QoS = {e.ApplicationMessage.QualityOfServiceLevel}");
                Console.WriteLine($"+ Retain = {e.ApplicationMessage.Retain}");
                Console.WriteLine();
            });
        }
    }
}
