using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;

namespace Infrastructure.Data.Repositories
{
    public class OwnerRepository : BaseRepository<Owner>, IOwnerRepository
    {
        private readonly ApplicationContext _context;
        public OwnerRepository(ApplicationContext context) : base(context)
        {
            _context = context;
        }
    }
}
