﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Models
{
    public class OwnerDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public static OwnerDto Create(Owner owner)
        {
            var dto = new OwnerDto();
            dto.Id = owner.Id;
            dto.Name = owner.Name;
            dto.LastName = owner.LastName;
            dto.Email = owner.Email;
            dto.Role = owner.Role;
            return dto;
        }
    }
}
