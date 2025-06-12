using Application.Interfaces;
using Application.Models;
using Application.Models.Request;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;

namespace Application.Services
{
    public class KayakService : IKayakService
    {
        private readonly IKayakRepository _kayakRepository;
        private readonly IOwnerRepository _ownerRepository;
        public KayakService(IKayakRepository kayakRepository, IOwnerRepository ownerRepository)
        {
            _kayakRepository = kayakRepository;
            _ownerRepository = ownerRepository;
        }

        public KayakDto Create(KayakCreateRequest request)
        {
            //Owner authenticatedOwner = _ownerRepository.GetById(ownerId);

            var kayak = new Kayak();
            kayak.Name = request.Name;
            kayak.Model = request.Model;
            kayak.Capacity = request.Capacity;
            kayak.Length = request.Length;
            kayak.Material = request.Material;
            kayak.PublicationDate = DateTime.Now;
            kayak.Color = request.Color;
            //kayak.Owner = authenticatedOwner;
            kayak.IsAvailable = false;

            var newKayak = _kayakRepository.Create(kayak);
            return KayakDto.Create(newKayak);
        }

        public void Update(int id, KayakUpdateRequest request)
        {
            var kayak = _kayakRepository.GetById(id);
            if (kayak == null)
            {
                throw new Exception($"Kayak con el ID: {id}, no ha sido encontrado.");
            }
            kayak.Name = request.Name;
            kayak.Model = request.Model;
            kayak.Capacity = request.Capacity;
            kayak.Length = request.Length;
            kayak.Material = request.Material;
            kayak.Color = request.Color;

            _kayakRepository.Update(kayak);
        }

        public void Delete(int id)
        {
            var kayak = _kayakRepository.GetById(id);
            if (kayak == null)
            {
                throw new NotFoundException($"Kayak con el ID: {id}, no ha sido encontrado.");
            }
            _kayakRepository.Delete(kayak);
        }

        public List<KayakDto> GetAll()
        {
            var kayaks = _kayakRepository.GetAll();
            return kayaks.Select(KayakDto.Create).ToList();
        }

        public KayakDto? GetById(int id)
        {
            var kayak = _kayakRepository.GetById(id) ?? throw new NotFoundException("Kayak no encontrado.");
            return KayakDto.Create(kayak);
        }

        public List<KayakDto> GetAvailableKayak()
        {
            var kayaks = _kayakRepository.GetAvailableKayak();

            if (kayaks == null || !kayaks.Any())
            {
                throw new NotFoundException("No hay kayaks disponibles en este momento.");
            }

            return kayaks.Select(KayakDto.Create).ToList();
        }

        public List<KayakDto> GetKayakByOwner(int ownerId)
        {
            var kayaks = _kayakRepository.GetByOwnerId(ownerId) ?? throw new NotFoundException("Dueño no encontrado.");

            if (kayaks == null || !kayaks.Any())
            {
                throw new NotFoundException("El dueño no tiene kayaks registrados.");
            }

            return kayaks.Select(KayakDto.Create).ToList();
        }

        public void EnableKayak(int kayakId)//, int ownerId)
        {
           // Owner? owner = _ownerRepository.GetById(ownerId) ?? throw new NotFoundException("Dueño no encontrado.");
            Kayak? kayak = _kayakRepository.GetById(kayakId) ?? throw new NotFoundException("Kayak no encontrado.");

          //  if (kayak.Owner.Id != ownerId)
          //  {
           //     throw new UnauthorizedAccessException("No tiene permiso para modificar el kayak.");
           // }

            _kayakRepository.EnableKayak(kayak);
        }

        public void DisableKayak(int kayakId)//, int ownerId)
        {
            //Owner? owner = _ownerRepository.GetById(ownerId) ?? throw new NotFoundException("Dueño no encontrado.");
            Kayak? kayak = _kayakRepository.GetById(kayakId) ?? throw new NotFoundException("Kayak no encontrado.");

          //  if (kayak.Owner.Id != ownerId)
          //  {
          //      throw new UnauthorizedAccessException("No tiene permiso para modificar el kayak.");
          //  }

            _kayakRepository.DisableKayak(kayak);
        }
    }
}
