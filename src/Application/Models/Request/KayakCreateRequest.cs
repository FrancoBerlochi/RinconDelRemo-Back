

namespace Application.Models.Request
{
    public class KayakCreateRequest
    {
        public string Name { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;   
        public string Type { get; set; } = string.Empty;
        public bool Status { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}
