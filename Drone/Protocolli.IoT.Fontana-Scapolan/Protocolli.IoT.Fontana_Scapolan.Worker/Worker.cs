using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Options;
using MQTTnet.Client.Subscribing;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Protocolli.IoT.Fontana_Scapolan.Worker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        private const string topic = "protocolliIot/drone1/stato";
        private const string topic_cmd = "protocolliIot/drone1/comando/+";
        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var wb = new WebClient();
            var sensor = new VirtualSensor();

            //Creazione MQTT Client
            var factory = new MqttFactory();
            var mqttClient = factory.CreateMqttClient();

            //Utilizzo connessione TCP
            var options = new MqttClientOptionsBuilder()
                            .WithTcpServer("192.168.104.86", 1883) //Port is optional
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
                //Ricezione del messaggio
                var data = sensor.getJson();
                //Pubblicazione del messaggio
                var message = new MqttApplicationMessageBuilder()
               .WithTopic(topic)
               .WithPayload(data)
               .WithExactlyOnceQoS()
               .Build();

                await mqttClient.PublishAsync(message, CancellationToken.None);

                Console.WriteLine(data);
                System.Threading.Thread.Sleep(20000);
            }
        }
    }
}
