using Application.Models;
using Application.Models.Request;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IKayakService
    {
        KayakDto Create(KayakCreateRequest request);
        void Update(int id, KayakUpdateRequest request);
        void Delete(int id);
        List<KayakDto> GetAll();
        KayakDto GetById(int id);
        List<KayakDto> GetAvailableKayak();
        List<KayakDto> GetKayakByOwner(string ownerId);
        void EnableKayak(int kayakId);//, int ownerId);
        void DisableKayak(int kayakId);//, int ownerId);
    }
}
