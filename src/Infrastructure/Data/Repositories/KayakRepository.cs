using Domain.Entities;
using Domain.Interfaces;

namespace Infrastructure.Data.Repositories
{
    public class KayakRepository : BaseRepository<Kayak>, IKayakRepository 
    {
        private readonly ApplicationContext _context;
        public KayakRepository(ApplicationContext context) : base(context)
        {
            _context = context;
        }
    }
}
