using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Models;
using Application.Models.Request;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IAdministratorService
    {
        Administrator Create(AdministratorCreateRequest request);
        void Update(string id, AdministratorUpdateRequest request);
        List<AdministratorDto> GetAll();
        AdministratorDto? GetById(string id);
        void Delete(string id);
    }
}
