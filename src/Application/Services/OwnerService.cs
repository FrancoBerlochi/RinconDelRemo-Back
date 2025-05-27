using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.Models;
using Application.Models.Request;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services
{
    public class OwnerService : IOwnerService
    {
        private readonly IOwnerRepository _ownerRepository;
        public OwnerService(IOwnerRepository ownerRepository)
        {
            _ownerRepository = ownerRepository;
        }

        public List<OwnerDto> GetAll()
        {
            var owners = _ownerRepository.GetAll();
            return owners.Select(OwnerDto.Create).ToList();
        }

        public OwnerDto? GetById(int id)
        {
            var owner = _ownerRepository.GetById(id) ?? throw new Exception("Dueño no encontrado.");
            return OwnerDto.Create(owner);
        }

        public Owner CreateOwner(OwnerCreateRequest request)
        {
            var owner = new Owner();
            owner.Name = request.Name;
            owner.LastName = request.LastName;
            owner.Email = request.Email;
            owner.Password = request.Password;
            owner.Phone = request.Phone;
            
            _ownerRepository.Create(owner);
            return owner;
        }

        public void Update(int id, OwnerUpdateRequest request)
        {
            var owner = _ownerRepository.GetById(id) ?? throw new Exception("Dueño no encontrado.");

            owner.Email = request.Email ?? owner.Email;
            owner.Password = request.Password ?? owner.Password;
            owner.Phone = request.Phone ?? owner.Phone;


            _ownerRepository.Update(owner);
        }

        public void Delete(int id)
        {
            var owner = _ownerRepository.GetById(id) ?? throw new Exception("Dueño no encontrado.");
            _ownerRepository.Delete(owner);
        }
    }
}
