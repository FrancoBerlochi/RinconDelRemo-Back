using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Enums;

namespace Domain.Interfaces
{
    public interface IKayakReservationRepository: IBaseRepository<KayakReservation>
    {
        IEnumerable<KayakReservation> GetFiltered(DateTime? date, int? tenantId);
        public List<KayakReservation> GetAllWithIncludes();
        IEnumerable<KayakReservation> GetByStatus(StatusReservation status);
    }
}
