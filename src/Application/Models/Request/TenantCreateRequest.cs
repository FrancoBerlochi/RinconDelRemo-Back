
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace Application.Models.Request
{
    public class TenantCreateRequest
    {
        [Required]
        [Column(TypeName = "nvarchar(100)")]
        public string Name { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "nvarchar(100)")]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "nvarchar(64)")]
        public string Password { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "nvarchar(11)")]
        public string Phone { get; set; } = string.Empty;
    }
}
