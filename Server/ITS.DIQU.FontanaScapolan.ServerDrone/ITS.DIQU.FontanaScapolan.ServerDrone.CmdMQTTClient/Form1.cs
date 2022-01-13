using ITS.DIQU.FontanaScapolan.ServerDrone.ApplicationCore.Entities;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Options;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ITS.DIQU.FontanaScapolan.ServerDrone.CmdMQTTClient
{
    public partial class Form1 : Form
    {
        MqttFactory factory = new MqttFactory();
        IMqttClient mqttClient;
        public Form1()
        {
            InitializeComponent();
            MQTTinit();
            List<string> listName = new List<string>();
            listName.Add("drone1");
            cmbDrone.DataSource = listName;
        }

        public async void MQTTinit() 
        {
            // Create a new MQTT client.
            mqttClient = factory.CreateMqttClient();
            // Creates a new client
            var options = new MqttClientOptionsBuilder()
                                            .WithTcpServer("192.168.104.86", 1883)
                                            .Build();

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

            await mqttClient.ConnectAsync(options, CancellationToken.None); // Since 3.0.5 with CancellationToken
            
        }

        private async void ckbDrone_CheckChangedAsync(object sender, EventArgs e)
        {
            if (ckbDrone.Checked == true)
            {
                var message = new MqttApplicationMessageBuilder()
                    .WithTopic("protocolliIot/" + cmbDrone.SelectedItem + "/comando/accensione")
                    .WithPayload("1")
                    .WithExactlyOnceQoS()
                    .WithRetainFlag()
                    .Build();
                await mqttClient.PublishAsync(message, CancellationToken.None); // Since 3.0.5 with CancellationToken
            }
            else
            {
                var message = new MqttApplicationMessageBuilder()
                    .WithTopic("protocolliIot/" + cmbDrone.SelectedItem + "/comando/accensione")
                    .WithPayload("0")
                    .WithExactlyOnceQoS()
                    .WithRetainFlag()
                    .Build();
                await mqttClient.PublishAsync(message, CancellationToken.None); // Since 3.0.5 with CancellationToken

            }
        }

        private async void ckbLED_CheckChangedAsync(object sender, EventArgs e)
        {
            if (ckbLED.Checked == true)
            {
                var message = new MqttApplicationMessageBuilder()
                    .WithTopic("protocolliIot/" + cmbDrone.SelectedItem + "/comando/led")
                    .WithPayload("1")
                    .WithExactlyOnceQoS()
                    .WithRetainFlag()
                    .Build();
                await mqttClient.PublishAsync(message, CancellationToken.None); // Since 3.0.5 with CancellationToken
            }
            else
            {
                var message = new MqttApplicationMessageBuilder()
                    .WithTopic("protocolliIot/" + cmbDrone.SelectedItem + "/comando/led")
                    .WithPayload("0")
                    .WithExactlyOnceQoS()
                    .WithRetainFlag()
                    .Build();
                await mqttClient.PublishAsync(message, CancellationToken.None); // Since 3.0.5 with CancellationToken

            }
        }

        private async void btnBase_Click(object sender, EventArgs e)
        {
            var message = new MqttApplicationMessageBuilder()
                    .WithTopic("protocolliIot/" + cmbDrone.SelectedItem + "/comando/base")
                    .WithPayload("1")
                    .WithExactlyOnceQoS()
                    .WithRetainFlag()
                    .Build();
            await mqttClient.PublishAsync(message, CancellationToken.None); // Since 3.0.5 with CancellationToken

        }
    }
}
