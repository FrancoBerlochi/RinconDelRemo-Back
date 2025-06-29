using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Models
{
    public class TenantDto
    {   
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Role { get; set; }
        public static TenantDto Create(Tenant tenant)
        {
            var dto = new TenantDto();
            dto.Id = tenant.Id;
            dto.Name = tenant.Name;
            dto.LastName = tenant.LastName;
            dto.Email = tenant.Email;
            dto.Role = tenant.Role;
            return dto;
        }

    }
}
