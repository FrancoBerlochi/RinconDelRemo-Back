using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Request
{
    public class KayakReservationUpdateRequest
    {
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public int KayakId { get; set; }
    }
}
