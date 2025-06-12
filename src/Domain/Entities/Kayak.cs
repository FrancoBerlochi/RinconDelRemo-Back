using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Enums;

namespace Domain.Entities
{
    public class Kayak
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Model { get; set; }
        public string Color { get; set; }
        public int Capacity { get; set; }
        public string Length { get; set; } 
        public Material Material { get; set; }
        public DateTime PublicationDate { get; set; }
        public bool IsAvailable { get; set; } = false;
        public Owner Owner { get; set; }
    }
}
