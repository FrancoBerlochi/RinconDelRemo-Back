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
        public string OwnerId { get; set; }
        public Owner? Owner { get; set; }
        public float Price { get; set; }
    }
}
