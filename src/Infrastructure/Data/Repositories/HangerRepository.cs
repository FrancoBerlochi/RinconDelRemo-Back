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
    public class HangerRepository : BaseRepository<Hanger>, IHangerRepository
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

        public IEnumerable<Hanger> GetByOwnerId(int ownerId)
        {
            return _context.Hangers
                .Include(h => h.Kayak)
                .Where(h => h.Kayak != null && h.Kayak.Owner.Id == ownerId)
                .ToList();
        }

        public bool IsKayakAssigned(int kayakId)
        {
            return _context.Hangers.Any(h => h.KayakId == kayakId);
        }
    }
}
