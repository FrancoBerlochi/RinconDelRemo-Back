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
    public interface IKayakReservationService
    {
        void Delete(int id);
        List<KayakReservationDto> GetAll();
        KayakReservationDto GetById(int id);
        void Update(int id, KayakReservationUpdateRequest kayakReservationUpdateRequest);
        KayakReservation Create(KayakReservationCreateRequest kayakReservationCreateRequest);
        List<KayakReservationDto> GetReservations(DateTime? date, int? tenantId);
    }
}
