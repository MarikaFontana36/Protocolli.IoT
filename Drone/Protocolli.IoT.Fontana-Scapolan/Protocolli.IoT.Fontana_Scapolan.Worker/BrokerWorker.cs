using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Options;
using MQTTnet.Client.Subscribing;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Protocolli.IoT.Fontana_Scapolan.Worker
{
    public class BrokerWorker : BackgroundService
    {
        private readonly ILogger<BrokerWorker> _logger;

        private const string topic = "protocolliIot/drone1/stato";
        private const string topic_cmd = "protocolliIot/drone1/comando/+";
        public BrokerWorker(ILogger<BrokerWorker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var wb = new WebClient();
            

            //Creazione MQTT Client
            var factory = new MqttFactory();
            var mqttClient = factory.CreateMqttClient();

            //Utilizzo connessione TCP
            var options = new MqttClientOptionsBuilder()
                            .WithTcpServer("192.168.104.69", 1883) //Port is optional
                            .WithCleanSession(false)//se perdo qualche comando quando mi disconnetto, lo ricevo quando mi riconnetto
                            .Build();

            //Consumo dei dati
            mqttClient.UseApplicationMessageReceivedHandler(e =>
            {
                Console.WriteLine("### ESECUZIONE COMANDO: ###");
                if (Encoding.UTF8.GetString(e.ApplicationMessage.Payload) == "1")//Controllo accensione drone
                {
                    if (e.ApplicationMessage.Topic == "protocolliIot/drone1/comando/accensione")
                    {
                        Console.WriteLine("Drone acceso");
                    }
                    if (e.ApplicationMessage.Topic == "protocolliIot/drone1/comando/led")
                    {
                        Console.WriteLine("Led acceso");
                    }
                    if (e.ApplicationMessage.Topic == "protocolliIot/drone1/comando/base")
                    {
                        Console.WriteLine("Il drone ritorna alla base");
                    }
                }
                else
                {
                    if (e.ApplicationMessage.Topic == "protocolliIot/drone1/comando/accensione")
                    {
                        Console.WriteLine("Drone spento");
                    }
                    if (e.ApplicationMessage.Topic == "protocolliIot/drone1/comando/led")
                    {
                        Console.WriteLine("Led spento");
                    }
                }
                Console.WriteLine("");
            });


            mqttClient.UseConnectedHandler(async e =>
            {
                Console.WriteLine("### CONNECTED WITH SERVER ###");

                //Sottoscrizione al topic_cmd
                await mqttClient.SubscribeAsync(new MqttClientSubscribeOptionsBuilder().WithTopicFilter(topic_cmd).Build());

                Console.WriteLine("### SUBSCRIBED ###");
            });

            //Riconnessione
            mqttClient.UseDisconnectedHandler(async e =>
            {
                Console.WriteLine("### DISCONNECTED FROM SERVER ###");
                await Task.Delay(TimeSpan.FromSeconds(5));

                try
                {
                    await mqttClient.ConnectAsync(options, CancellationToken.None);
                }
                catch
                {
                    Console.WriteLine("### RECONNECTING FAILED ###");
                }
            });
            await mqttClient.ConnectAsync(options, CancellationToken.None);

            while (true)
            {
                ConnectionFactory factoryAMQP = new ConnectionFactory();
                factoryAMQP.Uri = new Uri("amqps://svrwymtt:6ELHGVZAv0opLBdkF8qHFxf2RfeMH44Q@rattlesnake.rmq.cloudamqp.com/svrwymtt");
                using (var connection = factoryAMQP.CreateConnection())
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
                        //Pubblicazione del messaggio
                        var message = new MqttApplicationMessageBuilder()
                       .WithTopic(topic)
                       .WithPayload(data)
                       .WithExactlyOnceQoS()
                       .WithRetainFlag(true) //L'ultimo msg del topic viene salvato
                       .Build();

                        await mqttClient.PublishAsync(message, CancellationToken.None);

                        Console.WriteLine(data);
                    };

                    //Dopo aver fatto il publish sul broker mqtt, avviene la conferma della ricezione del dato
                    channel.BasicConsume(queue: "sensor",
                                         autoAck: true,
                                         consumer: consumer);
                }
                
                System.Threading.Thread.Sleep(20000);
            }
        }
    }
}
