using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class KayakReservationHistoryDto
    {
        public int ReservationId { get; set; } 
        public string KayakName { get; set; }
        public string TenantFullName { get; set; }
        public DateTime? CheckInTime { get; set; }
        public DateTime? CheckOutTime { get; set; }
        public string Estado { get; set; }
    }

}
