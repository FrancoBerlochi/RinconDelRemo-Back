using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Models
{
    public class AttendantDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public static AttendantDto Create(Attendant attendant)
        {
            var dto = new AttendantDto();
            dto.Id = attendant.Id;
            dto.Name = attendant.Name;
            dto.LastName = attendant.LastName;
            dto.Email = attendant.Email;
            return dto;

        }
    }
}
