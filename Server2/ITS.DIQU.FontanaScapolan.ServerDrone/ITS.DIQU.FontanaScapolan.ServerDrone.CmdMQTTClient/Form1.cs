using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace ITS.DIQU.FontanaScapolan.ServerDrone.CmdMQTTClient
{
    public partial class Form1 : Form
    {
        ConnectionFactory factory = new ConnectionFactory();
        public Form1()
        {
            InitializeComponent();
            AMQPinit();
            List<string> listName = new List<string>();
            listName.Add("drone1");
            cmbDrone.DataSource = listName;
        }

        public async void AMQPinit() 
        {
            
            factory.Uri = new Uri("amqps://svrwymtt:6ELHGVZAv0opLBdkF8qHFxf2RfeMH44Q@rattlesnake.rmq.cloudamqp.com/svrwymtt");
            
        }

        private async void ckbDrone_CheckChangedAsync(object sender, EventArgs e)
        {
            if (ckbDrone.Checked == true)
            {
                using (var connection = factory.CreateConnection())
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: "command",
                                         durable: true,
                                         exclusive: false,
                                         autoDelete: false,
                                         arguments: null);

                    string message = "1";
                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(exchange: "",
                                         routingKey: "comando."+ cmbDrone.SelectedText + ".accensione",
                                         basicProperties: null,
                                         body: body);
                }
            }
            else
            {
                using (var connection = factory.CreateConnection())
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: "command",
                                         durable: true,
                                         exclusive: false,
                                         autoDelete: false,
                                         arguments: null);

                    string message = "0";
                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(exchange: "",
                                         routingKey: "comando." + cmbDrone.SelectedText + ".accensione",
                                         basicProperties: null,
                                         body: body);
                }
            }
        }

        private async void ckbLED_CheckChangedAsync(object sender, EventArgs e)
        {
            if (ckbLED.Checked == true)
            {
                using (var connection = factory.CreateConnection())
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: "command",
                                         durable: true,
                                         exclusive: false,
                                         autoDelete: false,
                                         arguments: null);

                    string message = "1";
                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(exchange: "",
                                         routingKey: "comando." + cmbDrone.SelectedText + ".led",
                                         basicProperties: null,
                                         body: body);
                }
            }
            else
            {
                using (var connection = factory.CreateConnection())
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: "command",
                                         durable: true,
                                         exclusive: false,
                                         autoDelete: false,
                                         arguments: null);

                    string message = "0";
                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(exchange: "",
                                         routingKey: "comando." + cmbDrone.SelectedText + ".led",
                                         basicProperties: null,
                                         body: body);
                }

            }
        }

        private async void btnBase_Click(object sender, EventArgs e)
        {
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "command",
                                     durable: true,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                string message = "1";
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "",
                                     routingKey: "comando." + cmbDrone.SelectedText + ".base",
                                     basicProperties: null,
                                     body: body);
            }

        }
    }
}
