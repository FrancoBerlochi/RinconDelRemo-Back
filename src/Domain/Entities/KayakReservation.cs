using System;
using System.Collections.Generic;
using System.Linq;
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
        public int TenantId { get; set; }
        public Tenant Tenant { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public StatusReservation StatusReservation { get; set; }= StatusReservation.Active;


    }
}
