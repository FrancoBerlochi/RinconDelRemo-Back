
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Domain.Entities
{
    public abstract class User
    {
        [Required]
        public string Id { get; set; } = string.Empty;

        [Column(TypeName = "nvarchar(100)")]
        [Required]
        public string Name { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        [Required]
        public string LastName { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        [Required]  
        [EmailAddress]
        public string Email { get; set; }

    }
}
