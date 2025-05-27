
using Domain.Entities;
namespace Application.Models
{
    public class KayakDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
        public int OwnerId { get; set; }

        public static KayakDto create(Kayak kayak)
        {
            var dto = new KayakDto();
            dto.Id = kayak.Id;
            dto.Name = kayak.Name;
            dto.Color = kayak.Color;
            dto.Type = kayak.Type;
            dto.Status = kayak.Status;
            dto.Description = kayak.Description;
            dto.OwnerId = kayak.OwnerId;
            return dto;
        }
    } 
    









 } 