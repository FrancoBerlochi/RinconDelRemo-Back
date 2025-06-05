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

        public Owner? GetByNameLastname(string name, string lastname)
        {
            return _context.Owners.SingleOrDefault(o => o.Name == name && o.LastName == lastname);
        }

        public Owner GetByOwnerEmail(string email)
        {
            return _context.Owners.SingleOrDefault(o => o.Email == email);
        }

        public Owner GetByOwnerPhone(string phone)
        {
            return _context.Owners.SingleOrDefault(o => o.Phone == phone);
        }
    }
}
