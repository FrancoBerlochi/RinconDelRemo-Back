using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Models.Request;
using Application.Models;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IOwnerService
    {
        List<OwnerDto> GetAll();
        OwnerDto GetById(int id);
        Owner CreateOwner(OwnerCreateRequest ownerCreateRequest);
        void Update(int id, OwnerUpdateRequest ownerUpdateRequest);
        void Delete(int id);

    }
}
