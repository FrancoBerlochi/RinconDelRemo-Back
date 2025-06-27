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
        void Delete(string id);
        List<AttendantDto> GetAll();
        AttendantDto GetById(string id);
        void Update(string id, AttendantUpdateRequest attendantUpdateRequest);
        Attendant Create(AttendantCreateRequest attendantCreateRequest);

        void CheckIn(int id);
        void CheckOut(int id);
    }
}
