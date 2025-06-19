using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.Models;
using Application.Models.Request;
using Domain.Entities;
using Domain.Enums;
using Domain.Exceptions;
using Domain.Interfaces;

namespace Application.Services
{
    public class KayakReservationService : IKayakReservationService
    {
        public readonly IKayakReservationRepository _kayakReservationRepository;
        public KayakReservationService (IKayakReservationRepository kayakReservationRepository)
        {
            _kayakReservationRepository = kayakReservationRepository;
        }

        public List<KayakReservationDto> GetAll()
        {
            var reservas = _kayakReservationRepository.GetAll();
            return reservas.Select(KayakReservationDto.Create).ToList();
        }
        public KayakReservationDto? GetById(int id)
        {
            var reserva = _kayakReservationRepository.GetById(id) ?? throw new NotFoundException("Reserva no encontrada.");
            return KayakReservationDto.Create(reserva);
        }
        public KayakReservation Create(KayakReservationCreateRequest request)
        {
            if (request.FechaFin <= request.FechaInicio)
            {
                throw new Exception("La fecha de fin debe ser posterior a la fecha de inicio.");
            }
            var reservation = new KayakReservation();
            reservation.KayakId = request.KayakId;
            reservation.TenantId = request.TenantId;
            reservation.FechaInicio = request.FechaInicio;
            reservation.FechaFin = request.FechaFin;
            reservation.StatusReservation = StatusReservation.Active;
            _kayakReservationRepository.Create(reservation);
            return reservation;
        }

        public void Update(int id, KayakReservationUpdateRequest request)
        { 
            var reserva = _kayakReservationRepository.GetById(id) ?? throw new NotFoundException("reserva no encontrada.");
            reserva.FechaInicio= request.FechaInicio;
            reserva.FechaFin= request.FechaFin;
            reserva.KayakId= request.KayakId;

            _kayakReservationRepository.Update(reserva);

        }
        public void Delete(int id)
        {
            var reserva = _kayakReservationRepository.GetById(id) ?? throw new NotFoundException("Reserva no encontrada.");
            _kayakReservationRepository.Delete(reserva);
        }
        
        public void CanceledReservation(int id)
        {
            var reserva = _kayakReservationRepository.GetById(id);
            if (reserva == null)
            {
                throw new Exception("La reserva no existe.");
            }
            if (reserva.StatusReservation == StatusReservation.Canceled)
            {
                throw new Exception("La reserva ya está cancelada.");
            }
            reserva.StatusReservation = StatusReservation.Canceled;
            _kayakReservationRepository.Update(reserva);
        }
    }
}
