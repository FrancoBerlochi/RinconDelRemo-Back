using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IKayakReservationRepository: IBaseRepository<KayakReservation>
    {
        IEnumerable<KayakReservation> GetFiltered(DateTime? date, int? tenantId);
        public List<KayakReservation> GetAllWithIncludes();
    }
}
