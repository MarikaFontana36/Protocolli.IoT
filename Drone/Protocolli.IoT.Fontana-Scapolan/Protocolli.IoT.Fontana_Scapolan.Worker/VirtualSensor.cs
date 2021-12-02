using Protocolli.IoT.Fontana_Scapolan.Worker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Protocolli.IoT.Fontana_Scapolan.Worker
{
    public class VirtualSensor
    {
        public string getJson()
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
            string data = JsonSerializer.Serialize(drone);
            return data;
        }

    }
}
