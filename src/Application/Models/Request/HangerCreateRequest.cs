using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Request
{
    public class HangerCreateRequest
    {
        public int Row { get; set; }
        public char Column { get; set; }
        public string OwnerId { get; set; } = string.Empty;
    }
}
