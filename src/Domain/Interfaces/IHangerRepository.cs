using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IHangerRepository : IBaseRepository<Hanger, int>
    {
        Hanger? FindByRowAndColumn(int row, char column);
        IEnumerable<Hanger> GetOccupied();
        IEnumerable<Hanger> GetByOwnerId(string ownerId);
    }
}
