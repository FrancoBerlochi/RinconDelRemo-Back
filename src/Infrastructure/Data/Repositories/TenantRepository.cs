using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repositories
{
    public class TenantRepository : BaseRepository<Tenant>, ITenantRepository
    {
        private readonly ApplicationContext _context;
        public TenantRepository(ApplicationContext context) : base(context)
        {
            _context = context;
        }

        
    }
}
