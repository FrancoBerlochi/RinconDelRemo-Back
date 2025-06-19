using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Models;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Data.Repositories
{
    public class KayakReservationRepository : BaseRepository<KayakReservation>, IKayakReservationRepository
    {
        private readonly ApplicationContext _context;
        public KayakReservationRepository(ApplicationContext context) : base(context)
        {
            _context = context;
        }


        public IEnumerable<KayakReservation> GetFiltered(DateTime? date, int? tenantId)
        {
            var query = _context.KayaksReservations
                .Include(r => r.Kayak)
                .Include(r => r.Tenant)
                .AsQueryable();

            if (date.HasValue) 
            {
                query = query.Where(r => r.FechaInicio.Date == date.Value);
            }

            if (tenantId.HasValue)
            {
                query = query.Where(r => r.TenantId == tenantId.Value);
            }

            return query.ToList();
        }

        public List<KayakReservation> GetAllWithIncludes()
        {
            return _context.KayaksReservations
                .Include(r => r.Kayak)
                .Include(r => r.Tenant)
                .ToList();
        }

    }
}
