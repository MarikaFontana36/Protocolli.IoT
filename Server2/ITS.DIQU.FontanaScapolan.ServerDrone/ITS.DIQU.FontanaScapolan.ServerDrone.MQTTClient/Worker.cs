using ITS.DIQU.FontanaScapolan.ServerDrone.ApplicationCore.Entities;
using ITS.DIQU.FontanaScapolan.ServerDrone.ApplicationCore.Interfaces.Services;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
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
            while (!stoppingToken.IsCancellationRequested)
            {

                //Creazione della connessione alla coda
                ConnectionFactory factory = new ConnectionFactory();
                factory.Uri = new Uri("amqps://svrwymtt:6ELHGVZAv0opLBdkF8qHFxf2RfeMH44Q@rattlesnake.rmq.cloudamqp.com/svrwymtt");
                using (var connection = factory.CreateConnection())
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: "sensor",
                                         durable: true,
                                         exclusive: false,
                                         autoDelete: false,
                                         arguments: null);

                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += async (model, ea) =>
                    {
                        var body = ea.Body.ToArray();
                        //Recupero del messaggio
                        string data = Encoding.UTF8.GetString(body);
                        Console.WriteLine("### RECEIVED APPLICATION MESSAGE ###");
                        Console.WriteLine(data);
                        var drone = new Drone();
                        drone = JsonSerializer.Deserialize<Drone>(data);
                        _dronesService.Insert(drone);
                        Console.WriteLine();
                    };

                    //Dopo aver fatto il publish sul broker mqtt, avviene la conferma della ricezione del dato
                    channel.BasicConsume(queue: "sensor",
                                         autoAck: true,
                                         consumer: consumer);
                }
                await Task .Delay(1000, stoppingToken);
            }
        }
    }
}
