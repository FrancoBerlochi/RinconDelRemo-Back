using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Request
{
    public class AdministratorCreateRequest
    {
        
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}
