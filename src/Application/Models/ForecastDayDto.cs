using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class ForecastDayDto
    {
        public string Fecha { get; set; } = string.Empty;
        public float TempMin { get; set; }
        public float TempMax { get; set; }
        public string WeatherMain { get; set; } = string.Empty;
        public string WeatherDescription { get; set; } = string.Empty;
        public float WindSpeed { get; set; }
    }
}
