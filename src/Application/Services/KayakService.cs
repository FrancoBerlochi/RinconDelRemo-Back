using Application.Interfaces;
using Application.Models;
using Application.Models.Request;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services
{
    public class KayakService : IKayakService
    {
        private readonly IKayakRepository _kayakRepository;
        //private readonly IOwnerRepository _ownerRepository;
        public KayakService(IKayakRepository kayakRepository) // ,IOwnerRepository ownerRepository)
        {
            _kayakRepository = kayakRepository;
            //_ownerRepository = ownerRepository;
        }

        public Kayak Create(KayakCreateRequest request)
        {
   
            var kayak = new Kayak();
            kayak.Name = request.Name;
            kayak.Description = request.Description;
            kayak.Type = request.Type;
            kayak.Color = request.Color;
            kayak.Status = request.Status;
            //kayak.OwnerId = (no lo se rick);

            return _kayakRepository.Create(kayak);

        }

        public void Update(int id, KayakUpdateRequest request)
        {
            var kayak = _kayakRepository.GetById(id);
            if (kayak == null)
            {
                throw new Exception($"Kayak con el ID: {id}, no ha sido encontrado.");
            }
            kayak.Name = request.Name;
            kayak.Description = request.Description;
            kayak.Type = request.Type;
            kayak.Color = request.Color;
            kayak.Status = request.Status;

            _kayakRepository.Update(kayak);

        }

        public void Delete(int id)
        {
            var kayak = _kayakRepository.GetById(id);
            if (kayak == null)
            {
                throw new Exception($"Kayak con el ID: {id}, no ha sido encontrado.");
            }
            _kayakRepository.Delete(kayak);
        }

        public List<KayakDto> GetAll()
        {
            var kayaks = _kayakRepository.GetAll();
            return kayaks.Select(a => new KayakDto
            {
                Id = a.Id,
                Name = a.Name,
                Status = a.Status,
                Description = a.Description,
                Color = a.Color,
                Type = a.Type,
                OwnerId = a.OwnerId,


            }).ToList();
        }

        public KayakDto GetById(int id)
        {
            var kayak = _kayakRepository.GetById(id);
            if (kayak == null)
            {
                throw new Exception($"Kayak con el ID: {id}, no ha sido encontrado");
            }
            return new KayakDto
            {
                Id = kayak.Id,
                Name = kayak.Name,
                Status = kayak.Status,
                Description = kayak.Description,
                Color = kayak.Color,
                Type = kayak.Type,
                OwnerId = kayak.OwnerId,
            };
        }

        public List<Kayak> GetAvailableKayak()
        {
            return _kayakRepository.GetAvailableKayak().ToList();
        }
    }
}
