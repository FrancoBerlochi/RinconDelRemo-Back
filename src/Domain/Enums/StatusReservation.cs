using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enums
{
    public enum StatusReservation
    {
        Pendiente,
        EnUso,   // Check-in realizado
        Finalizada,   // Check-out realizado
        Cancelada
    }
}
