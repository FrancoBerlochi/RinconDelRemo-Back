using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Enums;

namespace Application.Models
{
    public class KayakReservationDto
    {
        public int Id { get; set; }
        public int KayakId { get; set; }
        public string TenantId { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public string? StatusReservation { get; set; }

        public static KayakReservationDto Create(KayakReservation kayakReservation)
        {
            var dto = new KayakReservationDto();
            dto.Id = kayakReservation.Id;
            dto.KayakId = kayakReservation.KayakId;
            dto.TenantId = kayakReservation.TenantId;
            dto.FechaInicio = kayakReservation.FechaInicio;
            dto.FechaFin = kayakReservation.FechaFin;
            dto.StatusReservation = kayakReservation.StatusReservation.ToString() ?? "";


            return dto;
        }
    }
}
