using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.Models
{
    public class WeatherData
    {
        public Forecast Forecast { get; set; }
    }

    public class Forecast
    {
        public List<HourlyForecast> Hourly { get; set; }
    }

    public class HourlyForecast
    {
        public Temperature Temperature_2m { get; set; }
        // Add other properties as needed
    }

    public class Temperature
    {
        public double Value { get; set; }
        public string Unit { get; set; }
    }
}