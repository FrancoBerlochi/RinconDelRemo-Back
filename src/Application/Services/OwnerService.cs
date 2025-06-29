using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.Models;
using Application.Models.Request;
using Domain.Entities;
using Domain.Exceptions;
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

        public OwnerDto? GetById(string id)
        {
            var owner = _ownerRepository.GetById(id) ?? throw new NotFoundException("Dueño no encontrado.");
            return OwnerDto.Create(owner);
        }

        public OwnerDto? GetByNameLastName(string name, string lastname)
        {
            var owner = _ownerRepository.GetByNameLastname(name, lastname) ?? throw new NotFoundException("Dueño no encontrado.");
            return OwnerDto.Create(owner);
        }

        public Owner CreateOwner(OwnerCreateRequest request)
        {
            var existingOwnerWithSameEmail = _ownerRepository.GetByOwnerEmail(request.Email);
            if (existingOwnerWithSameEmail != null)
            {
                throw new Exception("Ya existe un Dueño con el mismo correo electrónico.");
            }

            var owner = new Owner();
            owner.Id = request.OwnerId;
            owner.Name = request.Name;
            owner.LastName = request.LastName;
            owner.Email = request.Email;
            owner.Role = request.Role;
            _ownerRepository.Create(owner);
            return owner;
        }

        public void Update(string id, OwnerUpdateRequest request)
        {
            var owner = _ownerRepository.GetById(id) ?? throw new NotFoundException("Dueño no encontrado.");

            owner.Email = request.Email ?? owner.Email;


            _ownerRepository.Update(owner);
        }

        public void Delete(string id)
        {
            var owner = _ownerRepository.GetById(id) ?? throw new NotFoundException("Dueño no encontrado.");
            _ownerRepository.Delete(owner);
        }
    }
}
