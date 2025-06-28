using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Models
{
    public class HangerStatusDto
    {
        public string Location { get; set; } = string.Empty;
        public bool IsOccupied { get; set; }
        public int? KayakId { get; set; }
    }
}
