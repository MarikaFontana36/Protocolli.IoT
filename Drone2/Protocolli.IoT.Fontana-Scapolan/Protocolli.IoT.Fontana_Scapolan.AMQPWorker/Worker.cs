using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Protocolli.IoT.Fontana_Scapolan.AMQPWorker
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
            while (!stoppingToken.IsCancellationRequested)
            {
                var sensor = new VirtualSensor();

                //Creazione della connessione alle code sensor e command
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

                    string message = sensor.getJson();
                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(exchange: "",
                                         routingKey: "sensor",
                                         basicProperties: null,
                                         body: body);
                }

                using (var connection1 = factory.CreateConnection())
                using (var channel1 = connection1.CreateModel())
                {
                    channel1.QueueDeclare(queue: "command",
                                         durable: true,
                                         exclusive: false,
                                         autoDelete: false,
                                         arguments: null);

                    var consumer = new EventingBasicConsumer(channel1);
                    consumer.Received += async (model, ea) =>
                    {
                        var body = ea.Body.ToArray();
                        //Recupero del messaggio
                        string data = Encoding.UTF8.GetString(body);
                        //Pubblicazione del messaggio

                        if (data == "1")//Controllo accensione drone
                        {
                            if (ea.RoutingKey == "comando.drone1.accensione")
                            {
                                Console.WriteLine("### ESECUZIONE COMANDO: ###");
                                Console.WriteLine("Drone acceso");
                            }
                            if (ea.RoutingKey == "comando.drone1.led")
                            {
                                Console.WriteLine("### ESECUZIONE COMANDO: ###");
                                Console.WriteLine("Led acceso");
                            }
                            if (ea.RoutingKey == "comando.drone1.base")
                            {
                                Console.WriteLine("### ESECUZIONE COMANDO: ###");
                                Console.WriteLine("Il drone ritorna alla base");
                            }
                        }
                        else
                        {
                            if (ea.RoutingKey == "comando.drone1.accensione")
                            {
                                Console.WriteLine("### ESECUZIONE COMANDO: ###");
                                Console.WriteLine("Drone spento");
                            }
                            if (ea.RoutingKey == "comando.drone1.led")
                            {
                                Console.WriteLine("### ESECUZIONE COMANDO: ###");
                                Console.WriteLine("Led spento");
                            }
                        }
                        Console.WriteLine("");

                        Console.WriteLine(data);
                    };

                    //Dopo aver fatto il publish sul broker, avviene la conferma della ricezione del dato
                    channel1.BasicConsume(queue: "command",
                                         autoAck: true,
                                         consumer: consumer);
                }

                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
