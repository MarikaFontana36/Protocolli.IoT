using System;

namespace ITS.DIQU.FontanaScapolan.ServerDrone.ApplicationCore.Entities
{
    public class Drone : Entity<int>
    {
        public DateTime Date { get; set; }

        public string Position { get; set; }

        public double Speed { get; set; }

        public int BatteryLevel { get; set; }

        public int IdDrone { get; set; }

        public int Time { get; set; }
    }
}
