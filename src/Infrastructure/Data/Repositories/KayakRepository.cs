using Domain.Entities;
using Domain.Interfaces;

namespace Infrastructure.Data.Repositories
{
    public class KayakRepository : BaseRepository<Kayak, int>, IKayakRepository
    {
        private readonly ApplicationContext _context;
        public KayakRepository(ApplicationContext context) : base(context)
        {
            _context = context;
        }
        public List<Kayak> GetAvailableKayak()
        {
            return _context.Kayaks.Where(k => k.IsAvailable == true).ToList();
        }

        public List<Kayak> GetByOwnerId(string ownerId)
        {
            return _context.Kayaks.Where(k => k.Owner.Id == ownerId).ToList();
        }

        public void EnableKayak(Kayak kayak)
        {
            kayak.IsAvailable = true;
            _context.SaveChanges();
        }

        public void DisableKayak(Kayak kayak)
        {
            kayak.IsAvailable = false;
            _context.SaveChanges();
        }

        public bool ExistsWithHangerId(int hangerId)
        {
            return _context.Kayaks.Any(k => k.HangerId == hangerId);
        }
    }
}
