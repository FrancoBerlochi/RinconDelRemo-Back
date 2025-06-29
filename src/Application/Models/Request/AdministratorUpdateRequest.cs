using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Request
{
    public class AdministratorUpdateRequest
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}
