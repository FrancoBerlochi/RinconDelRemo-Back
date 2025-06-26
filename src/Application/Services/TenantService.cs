using Application.Interfaces;
using Application.Models;
using Application.Models.Request;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services
{
    public class TenantService : ITenantService
    {
        private readonly ITenantRepository _tenantRepository;
        public TenantService(ITenantRepository tenantRepository)
        {
            _tenantRepository = tenantRepository;
        }
        public Tenant Create(TenantCreateRequest tenantCreateRequest)
        {
            var tenant = new Tenant
            {
                Name = tenantCreateRequest.Name,
                LastName = tenantCreateRequest.LastName,
                Email = tenantCreateRequest.Email,
                Password = tenantCreateRequest.Password,
                Phone = tenantCreateRequest.Phone
            };
            _tenantRepository.Create(tenant);
            return tenant;
        }

        public void Update(int id, TenantUpdateRequest tenantUpdateRequest)
        {
            var tenant = _tenantRepository.GetById(id);
            if (tenant == null)
            {
                throw new Exception($"Tenant with id {id} not found"); //Cambiar por un custom exception
            }
            tenant.Email = tenantUpdateRequest.Email;
            tenant.Password = tenantUpdateRequest.Password;
            tenant.Phone = tenantUpdateRequest.Phone;
            _tenantRepository.Update(tenant);
        }
        
        public List<TenantDto> GetAll()
        {
            var tenants = _tenantRepository.GetAll();
            return tenants.Select(t => new TenantDto
            {
                Id = t.Id,
                Name = t.Name,
                LastName = t.LastName,
                Email = t.Email,
                Phone = t.Phone
            }).ToList();
        }

        public TenantDto GetById(int id)
        {
            var tenant = _tenantRepository.GetById(id);
            if (tenant == null)
            {
                throw new Exception($"Tenant with id {id} not found"); //Cambiar por un custom exception
            }
            return new TenantDto
            {
                Id = tenant.Id,
                Name = tenant.Name,
                LastName = tenant.LastName,
                Email = tenant.Email,
                Phone = tenant.Phone
            };
        }
        public void Delete(int id)
        {
            var tenant = _tenantRepository.GetById(id);
            if (tenant == null)
            {
                throw new Exception($"Tenant with id {id} not found"); //Cambiar por un custom exception
            }
            _tenantRepository.Delete(tenant);
        }
    }
}
