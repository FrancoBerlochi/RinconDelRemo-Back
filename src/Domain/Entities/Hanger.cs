using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Hanger
    {
        public int Id { get; set; }
        public int Row { get; set; }
        public char Column { get; set; }
        public bool IsOccupied { get; set; }
        public int? KayakId { get; set; }
        public Kayak? Kayak { get; set; }
    }
}
