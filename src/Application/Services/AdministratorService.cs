using System;
using System.Collections.Generic;
using System.Linq;
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
    public class AdministratorService : IAdministratorService
    {
        private readonly IAdministratorRepository _administratorRepository;
        public AdministratorService(IAdministratorRepository administratorRepository)
        {
            _administratorRepository = administratorRepository;
        }
        public Administrator Create(AdministratorCreateRequest request)
        {
            var administrator = new Administrator
            {
                Id = request.Id,
                Name = request.Name,
                LastName = request.LastName,
                Email = request.Email,
                Role = request.Role,
            };
            _administratorRepository.Create(administrator);
            return administrator;
        }

        public void Update(string id, AdministratorUpdateRequest request)
        {
            var administrator = _administratorRepository.GetById(id);
            if (administrator == null)
            {
                throw new NotFoundException($"Administrator with id {id} not found"); 
            }
            administrator.Email = request.Email;
            _administratorRepository.Update(administrator);
        }

        public List<AdministratorDto> GetAll()
        {
            var administrator = _administratorRepository.GetAll();
            return administrator.Select(AdministratorDto.Create).ToList();
        }

        public AdministratorDto? GetById(string id)
        {
            var administrator = _administratorRepository.GetById(id) ?? throw new NotFoundException("Admin no encontrado.");
            return AdministratorDto.Create(administrator);
        }
        public void Delete(string id)
        {
            var administrator = _administratorRepository.GetById(id) ?? throw new NotFoundException("Admin no encontrado.");
            _administratorRepository.Delete(administrator);
        }
    }
}
