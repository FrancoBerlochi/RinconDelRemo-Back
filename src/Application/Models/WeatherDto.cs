using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class WeatherDto
    {
        public string City { get; set; } = "Rosario";
        public string Description { get; set; } = string.Empty;
        public float Temperature { get; set; }
        public float WindSpeed { get; set; }
        public float minTemperature { get; set; }
        public float maxTemperature { get; set; }
        public List<string> Weather { get; set; } = new List<string>();

    }
}
