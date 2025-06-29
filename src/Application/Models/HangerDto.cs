using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Models
{
    public class HangerDto
    {
        public int Id { get; set; }
        public int Row { get; set; }
        public char Column { get; set; }
        public bool IsOccupied { get; set; }
        public float Price { get; set; }

        public static HangerDto Create(Hanger hanger)
        {
            return new HangerDto
            {
                Id = hanger.Id,
                Column = hanger.Column,
                Row = hanger.Row,
                IsOccupied = hanger.IsOccupied,
                Price = hanger.Price,
            };
        }
    }
}
