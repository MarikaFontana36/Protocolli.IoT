using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Protocolli.IoT.Fontana_Scapolan.Worker.Models
{
    internal class Drone
    {
        public int Id { get; set; }
        public string Date { get; set; }
        public string Position { get; set; }
        public double Speed { get; set; }
        public int BatteryLevel { get; set; }
    }
}
