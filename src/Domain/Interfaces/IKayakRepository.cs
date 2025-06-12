
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IKayakRepository : IBaseRepository<Kayak>
    {
        List<Kayak> GetAvailableKayak();
        List<Kayak> GetByOwnerId(int ownerId);
        void EnableKayak(Kayak kayak);
        void DisableKayak(Kayak kayak);
    }
}
