using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeassurementRest.Models
{
    public class Meassurement
    {
        public int Id { get; set; }
        public double Pressure { get; set; }
        public double Humidity { get; set; }
        public double Temperature { get; set; }
        public DateTime TimeStamp { get; set; }

        
        public Meassurement(int id, double pressure, double humidity, double temperature)
        {
            Id = id;
            Pressure = pressure;
            Humidity = humidity;
            Temperature = temperature;
            TimeStamp = new DateTime();
        }

        public Meassurement()
        {
        }
    }
}
