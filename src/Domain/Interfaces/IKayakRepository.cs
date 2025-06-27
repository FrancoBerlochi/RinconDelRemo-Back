
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IKayakRepository : IBaseRepository<Kayak, int>
    {
        List<Kayak> GetAvailableKayak();
        List<Kayak> GetByOwnerId(string ownerId);
        void EnableKayak(Kayak kayak);
        void DisableKayak(Kayak kayak);
    }
}
