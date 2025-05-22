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
    public interface IAttendantService
    {
        void Delete(int id);
        List<AttendantDto> GetAll();
        AttendantDto GetById(int id);
        void Update(int id, AttendantUpdateRequest attendantUpdateRequest);
        Attendant Create(AttendantCreateRequest attendantCreateRequest);
    }
}
