using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Domain.Enums;

namespace Domain.Entities
{
    public class KayakReservation
    {
        public int Id {  get; set; }
        public int KayakId { get; set; }
        public Kayak Kayak { get; set; }
        public string? TenantId { get; set; }
        public Tenant Tenant { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public StatusReservation StatusReservation { get; set; }= StatusReservation.Active;

 
        public bool IsCheckedIn {  get; set; } = false;
        public bool IsCheckedOut { get; set;} = false;
        public DateTime? CheckInTime { get; set; }
        public DateTime? CheckOutTime { get;set; }


    }
}
