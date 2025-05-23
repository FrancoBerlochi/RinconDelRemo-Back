using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Request
{
    public class TenantCreateDto
    {
        [Required]
        [Column(TypeName = "nvarchar(100)")]
        public string Name { get; set; } 

        [Required]
        [Column(TypeName = "nvarchar(100)")]
        public string LastName { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(100)")]
        public string Email { get; set; } 

        [Required]
        [Column(TypeName = "nvarchar(64)")]
        public string Password { get; set; } 

        [Required]
        [Column(TypeName = "nvarchar(11)")]
        public string Phone { get; set; } 
    }
}
