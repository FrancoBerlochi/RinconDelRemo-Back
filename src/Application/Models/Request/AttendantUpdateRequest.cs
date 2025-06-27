using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Request
{
    public class AttendantUpdateRequest
    {
        [EmailAddress]
        public string Email { get; set; } = string.Empty;


    }
}
