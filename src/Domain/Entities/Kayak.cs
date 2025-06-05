
namespace Domain.Entities
{
    public class Kayak
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public string Type { get; set; }
        public bool Status { get; set; } = false;
        public string Description { get; set; }
        public int OwnerId { get; set; }
        public Owner Owner { get; set; }

    }
}
