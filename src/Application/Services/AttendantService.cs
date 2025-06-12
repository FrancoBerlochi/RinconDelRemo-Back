using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.Models;
using Application.Models.Request;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services
{
    public class AttendantService : IAttendantService
    {
        private readonly IAttendantRepository _attendantRepository;
        private readonly IKayakReservationRepository _kayakReservationRepository;
        private readonly IKayakRepository _kayakRepository;
        public AttendantService(IAttendantRepository attendantRepository, IKayakReservationRepository kayakReservationRepository, IKayakRepository kayakRepository)
        {
            _attendantRepository = attendantRepository;
            _kayakReservationRepository = kayakReservationRepository;
            _kayakRepository = kayakRepository;
        }

        public Attendant Create(AttendantCreateRequest attendantCreateRequest)
        {
            var attendant = new Attendant
            {
                Name = attendantCreateRequest.Name,
                LastName = attendantCreateRequest.LastName,
                Email = attendantCreateRequest.Email,
                Password = attendantCreateRequest.Password,
                Phone = attendantCreateRequest.Phone
            };
            _attendantRepository.Create(attendant);
            return attendant;
        }

        public void Update(int id, AttendantUpdateRequest attendantUpdateRequest)
        {
            var attendant = _attendantRepository.GetById(id);
            if (attendant == null)
            {
                throw new Exception($"Attendant with id {id} not found"); //Cambiar por un custom exception
            }
            attendant.Email = attendantUpdateRequest.Email;
            attendant.Password = attendantUpdateRequest.Password;
            attendant.Phone = attendantUpdateRequest.Phone;
            _attendantRepository.Update(attendant);
        }

        public List<AttendantDto> GetAll()
        {
            var attendants = _attendantRepository.GetAll();
            return attendants.Select(a => new AttendantDto
            {
                Id = a.Id,
                Name = a.Name,
                LastName = a.LastName,
                Email = a.Email,
                Phone = a.Phone
            }).ToList();
        }

        public AttendantDto GetById(int id)
        {
            var attendant = _attendantRepository.GetById(id);
            if (attendant == null)
            {
                throw new Exception($"Attendant with id {id} not found"); //Cambiar por un custom exception
            }
            return new AttendantDto
            {
                Id = attendant.Id,
                Name = attendant.Name,
                LastName = attendant.LastName,
                Email = attendant.Email,
                Phone = attendant.Phone
            };
        }

        public void Delete(int id)
        {
            var attendant = _attendantRepository.GetById(id);
            if (attendant == null)
            {
                throw new Exception($"Attendant with id {id} not found"); //Cambiar por un custom exception
            }
            _attendantRepository.Delete(attendant);
        }

        public void CheckIn(int id)
        {
            var reservation = _kayakReservationRepository.GetById(id);
            if (reservation == null)
            {
                throw new Exception("Reservation not found");
            }
            if (reservation.IsCheckedIn)
            {
                throw new Exception("Check-in has already been done");
            }

            reservation.IsCheckedIn = true;
            reservation.CheckInTime = DateTime.Now;

            var kayak = _kayakRepository.GetById(reservation.KayakId);
            kayak.IsAvailable = true;

            _kayakReservationRepository.Update(reservation);
            _kayakRepository.Update(kayak);
        }

        public void CheckOut(int id)
        {
            var reservation = _kayakReservationRepository.GetById(id);
            if (reservation == null)
            {
                throw new Exception("Reservation not found");
            }
            if (!reservation.IsCheckedOut)
            {
                throw new Exception("You must check-in first");
            }
            if (reservation.IsCheckedOut)
            {
                throw new Exception("Check-out has already been done");
            }

            reservation.IsCheckedOut = true;
            reservation.CheckOutTime = DateTime.Now;

            var kayak = _kayakRepository.GetById(reservation.KayakId);
            kayak.IsAvailable = false;

            _kayakReservationRepository.Update(reservation);
            _kayakRepository.Update(kayak);
        }
    }
}
