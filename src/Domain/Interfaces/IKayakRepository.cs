
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IKayakRepository : IBaseRepository<Kayak>
    {
        List<Kayak> GetAvailableKayak();
    }
}
