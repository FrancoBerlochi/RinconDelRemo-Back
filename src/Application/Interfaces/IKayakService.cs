using Application.Models;
using Application.Models.Request;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IKayakService
    {
        Kayak Create(KayakCreateRequest request);

        void Update(int id, KayakUpdateRequest request);

        void Delete(int id);

        List<KayakDto> GetAll();

        KayakDto GetById(int id);
    }
}
