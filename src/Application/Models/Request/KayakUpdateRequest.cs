using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Request
{
    public class KayakUpdateRequest
    {
        public string Name { get; set; }
        public string Color { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
    }
}
