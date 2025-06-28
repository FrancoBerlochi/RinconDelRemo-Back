
using Domain.Entities;
namespace Application.Models
{
    public class KayakDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Model { get; set; }
        public string? Color { get; set; }
        public int? Capacity { get; set; }
        public string? Length { get; set; }
        public string? Material { get; set; }
        public DateTime? PublicationDate { get; set; }
        public bool IsAvailable { get; set; } = false;
        public string? OwnerId { get; set; }

        public static KayakDto Create(Kayak kayak)
        {
            var dto = new KayakDto();
            dto.Id = kayak.Id;
            dto.Name = kayak.Name;
            dto.Model = kayak.Model;
            dto.Color = kayak.Color;
            dto.Capacity = kayak.Capacity;
            dto.Length = kayak.Length;
            dto.Material = kayak.Material.ToString() ?? "";
            dto.PublicationDate = kayak.PublicationDate;
            dto.IsAvailable = kayak.IsAvailable;
            dto.OwnerId = kayak.OwnerId;
            return dto;
        }
    } 
 } 