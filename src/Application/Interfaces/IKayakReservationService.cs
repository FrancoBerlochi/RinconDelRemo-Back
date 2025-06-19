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
    public interface IKayakReservationService
    {
        public List<KayakReservationDto> GetAll();
        public KayakReservationDto? GetById(int id);
        public KayakReservation Create(KayakReservationCreateRequest request);
        public void Update(int id, KayakReservationUpdateRequest request);
        public void Delete(int id);
        public void CanceledReservation(int id);
    }
}
