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
        public KayakReservationDto Create(KayakReservationCreateRequest request)
        {
           

            var reservation = new KayakReservation();
            reservation.KayakId = request.KayakId;
            reservation.TenantId = request.TenantId;
            reservation.FechaInicio = request.FechaInicio;
            reservation.FechaFin = request.FechaFin;
            var newReservation = _kayakReservationRepository.Create(reservation);
            return KayakReservationDto.Create(newReservation);
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


        public List<KayakReservationDto> GetReservations(DateTime? date, string? tenantId)
        {
            var reservas = _kayakReservationRepository.GetFiltered(date, tenantId);
            return reservas.Select(KayakReservationDto.Create).ToList();
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

        public List<KayakReservationHistoryDto> GetCheckInCheckOutHistory()
        {
            var respuesta = _kayakReservationRepository.GetAllWithIncludes();
            return respuesta.Select(r => new KayakReservationHistoryDto
            {
                KayakName = r.Kayak.Name,
                TenantFullName = $"{r.Tenant.Name} {r.Tenant.LastName}",
                CheckInTime = r.FechaInicio,
                CheckOutTime = r.FechaFin,
                Estado = r.StatusReservation.ToString()
            }).ToList();

        }

        public List<KayakReservationDto> GetActiveReservations()
        {
            var reservas = _kayakReservationRepository.GetByStatus(StatusReservation.Active);
            if (reservas == null)
            {
                throw new NotFoundException("No se han encontrado Reservas activas.");
            }
            return reservas.Select(KayakReservationDto.Create).ToList();
        }

        public List<KayakReservationDto> GetCancelledReservations()
        {
            var reservas = _kayakReservationRepository.GetByStatus(StatusReservation.Canceled);
            if (reservas == null)
            {
                throw new NotFoundException("No se han encontrado Reservas canceladas.");
            }
            return reservas.Select(KayakReservationDto.Create).ToList();
        }

        public List<KayakReservationDto> GetCompletedReservations()
        {
            var reservas = _kayakReservationRepository.GetByStatus(StatusReservation.Finished);
            if (reservas == null)
            {
                throw new NotFoundException("No se han encontrado Reservas finalizadas.");
            }
            return reservas.Select(KayakReservationDto.Create).ToList();
        }

       
    }
}
