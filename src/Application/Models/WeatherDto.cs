using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class WeatherDto
    {
        public string City { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public float Temperature { get; set; }
    }
}
