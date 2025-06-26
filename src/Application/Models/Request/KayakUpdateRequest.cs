

using Domain.Enums;

namespace Application.Models.Request
{
    public class KayakUpdateRequest
    {
        public string Name { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
        public int Capacity { get; set; }
        public string Length { get; set; } = string.Empty;
        public Material Material { get; set; }
    }
}
