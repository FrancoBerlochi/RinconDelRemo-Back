using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;

namespace Infrastructure.Data.Repositories
{
    public class AdministratorRepository: BaseRepository<Administrator, string>, IAdministratorRepository
    {
        private readonly ApplicationContext _context;
        public AdministratorRepository(ApplicationContext context) : base(context)
        {
            _context = context;
        }
    }
}
