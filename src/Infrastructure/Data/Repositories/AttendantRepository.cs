using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;

namespace Infrastructure.Data.Repositories
{
    public class AttendantRepository : BaseRepository<Attendant>, IAttendantRepository
    {
        private readonly ApplicationContext _context;
        public AttendantRepository(ApplicationContext context) : base(context)
        {
            _context = context;
        }
    }
}
