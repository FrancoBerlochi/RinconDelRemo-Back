using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories
{
    public class HangerRepository : BaseRepository<Hanger, int>, IHangerRepository
    {
        private readonly ApplicationContext _context;
        public HangerRepository(ApplicationContext context) : base(context)
        {
            _context = context;
        }

        public Hanger? FindByRowAndColumn(int row, char column)
        {
            return _context.Hangers.FirstOrDefault(h => h.Row == row && h.Column == column);
        }

        public IEnumerable<Hanger> GetOccupied()
        {
            return _context.Hangers.Where(h => h.IsOccupied).ToList();
        }

        public IEnumerable<Hanger> GetByOwnerId(string ownerId)
        {
            return _context.Hangers
                .Include(h => h.Owner)
                .Where(h => h.Owner != null && h.Owner.Id == ownerId)
                .ToList();
        }
    }
}
