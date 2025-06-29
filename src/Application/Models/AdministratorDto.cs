using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Models
{
    public class AdministratorDto
    {
        public string Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public static AdministratorDto Create(Administrator administrator)
        {
            var dto = new AdministratorDto();
            dto.Id = administrator.Id;
            dto.Name = administrator.Name;
            dto.LastName = administrator.LastName;
            dto.Email = administrator.Email;
            return dto;

        }
    }
}
