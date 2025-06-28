using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.Models;
using Application.Models.Request;
using Domain.Entities;
using Domain.Exceptions;
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
                Id = attendantCreateRequest.Id,
                Name = attendantCreateRequest.Name,
                LastName = attendantCreateRequest.LastName,
                Email = attendantCreateRequest.Email,
            };
            _attendantRepository.Create(attendant);
            return attendant;
        }

        public void Update(string id, AttendantUpdateRequest attendantUpdateRequest)
        {
            var attendant = _attendantRepository.GetById(id);
            if (attendant == null)
            {
                throw new Exception($"Attendant with id {id} not found"); //Cambiar por un custom exception
            }
            attendant.Email = attendantUpdateRequest.Email;
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
            }).ToList();
        }

        public AttendantDto GetById(string id)
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
            };
        }

        public void Delete(string id)
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
                throw new NotFoundException("Reserva no encontrada");
            }
            if (reservation.IsCheckedIn)
            {
                throw new Exception("Ya se realizó el check-in");
            }

            reservation.IsCheckedIn = true;
            reservation.CheckInTime = DateTime.UtcNow;

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
                throw new NotFoundException("Reserva no encontrada");
            }
            if (!reservation.IsCheckedIn)
            {
                throw new Exception("Primero debe realizar el check-in");
            }
            if (reservation.IsCheckedOut)
            {
                throw new Exception("Ya se realizó el check-out ");
            }

            reservation.IsCheckedOut = true;
            reservation.CheckOutTime = DateTime.UtcNow;

            var kayak = _kayakRepository.GetById(reservation.KayakId);
            kayak.IsAvailable = false;

            _kayakReservationRepository.Update(reservation);
            _kayakRepository.Update(kayak);
        }
    }
}
