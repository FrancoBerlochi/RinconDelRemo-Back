using Application.Interfaces;
using Application.Models;
using Application.Models.Request;
using Domain.Entities;
using Domain.Interfaces;
using System.Net.Http;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;

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
                Id = tenantCreateRequest.Id,
                Name = tenantCreateRequest.Name,
                LastName = tenantCreateRequest.LastName,
                Email = tenantCreateRequest.Email,
                Role = tenantCreateRequest.Role

            };
            _tenantRepository.Create(tenant);
            return tenant;
        }

        public void Update(string id, TenantUpdateRequest tenantUpdateRequest)
        {
            //var infoRoles = user.Claims.ToDictionary(c => c.Type, c => c.Value).Where(c => c.Key == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role").ToList();
            //var infoTipoUsuario = user.Claims.ToDictionary(c => c.Type, c => c.Value).Where(c => c.Key == "Tipo de usuario").ToList();
            //if (infoRoles[0].Value == "admin" || infoRoles[0].Value == "encargado")
            //{
                var tenant = _tenantRepository.GetById(id);
                if (tenant == null)
                {
                    throw new Exception($"Tenant with id {id} not found"); //Cambiar por un custom exception
                }
                tenant.Email = tenantUpdateRequest.Email;
                _tenantRepository.Update(tenant);
            //}
            //else if (infoTipoUsuario[0].Value == "Cliente")
            //{
            //    var claimId = user.Claims.ToDictionary(c => c.Type, c => c.Value).Where(c => c.Key == "http://schemas.microsoft.com/identity/claims/objectidentifier").ToList();
            //    var tenant2 = _tenantRepository.GetById(claimId[0].Value);
            //    if (tenant2 == null)
            //    {
            //        throw new Exception($"Tenant with id {claimId[0].Value} not found"); //Cambiar por un custom exception
            //    }
            //    tenant2.Email = tenantUpdateRequest.Email;
            //    tenant2.Password = tenantUpdateRequest.Password;
            //    tenant2.Phone = tenantUpdateRequest.Phone;
            //    _tenantRepository.Update(tenant2);
            //}
            //else
            //{
            //    throw new Exception($"You are not a tenant");
            //}   
        }
        
        public List<TenantDto> GetAll()
        {
            var tenant = _tenantRepository.GetAll();
            return tenant.Select(TenantDto.Create).ToList();
        }

        public TenantDto GetById(string id)
        {
          //  var infoRoles = user.Claims.ToDictionary(c => c.Type, c => c.Value).Where(c => c.Key == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role").ToList();
           // var infoTipoUsuario = user.Claims.ToDictionary(c => c.Type, c => c.Value).Where(c => c.Key == "Tipo de usuario").ToList();
           // if (infoRoles[0].Value == "admin" || infoRoles[0].Value == "encargado")
          //  {
                var tenant = _tenantRepository.GetById(id);
                if (tenant == null)
                {
                    throw new Exception($"Tenant with id {id} not found"); 
                }
                return new TenantDto
                {
                    Id = tenant.Id,
                    Name = tenant.Name,
                    LastName = tenant.LastName,
                    Email = tenant.Email,
                    Role = tenant.Role
  
                };
           // }
        //    else if (infoTipoUsuario[0].Value != "Cliente")
         //   {
        //        throw new Exception($"You are not a tenant");
         //   }
            //var claimId = user.Claims.ToDictionary(c => c.Type, c => c.Value).Where(c => c.Key == "http://schemas.microsoft.com/identity/claims/objectidentifier").ToList();
            //var tenant2 = _tenantRepository.GetById(claimId[0].Value);
            //if (tenant2 == null)
            //{
            //    throw new Exception($"Tenant with id {claimId[0].Value} not found");
            //}
            //return new TenantDto
            //{
            //    Id = tenant2.Id,
            //    Name = tenant2.Name,
            //    LastName = tenant2.LastName,
            //    Email = tenant2.Email,
            //    Phone = tenant2.Phone
            //};
        }
        public void Delete(string id)
        {
            //var infoRoles = user.Claims.ToDictionary(c => c.Type, c => c.Value).Where(c => c.Key == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role").ToList();
            //var infoTipoUsuario = user.Claims.ToDictionary(c => c.Type, c => c.Value).Where(c => c.Key == "Tipo de usuario").ToList();
            //if (infoRoles[0].Value == "admin" || infoRoles[0].Value == "encargado")
            //{
                var tenant = _tenantRepository.GetById(id);
                if (tenant == null)
                {
                   throw new Exception($"Tenant with id {id} not found"); //Cambiar por un custom exception
                }
                _tenantRepository.Delete(tenant);
            //}
            //else if(infoTipoUsuario[0].Value == "Cliente")
            //{
            //    var claimId = user.Claims.ToDictionary(c => c.Type, c => c.Value).Where(c => c.Key == "http://schemas.microsoft.com/identity/claims/objectidentifier").ToList();
            //    var tenant2 = _tenantRepository.GetById(claimId[0].Value);
            //    if (tenant2 == null)
            //    {
            //        throw new Exception($"Tenant with id {claimId[0].Value} not found"); //Cambiar por un custom exception
            //    }
            //    _tenantRepository.Delete(tenant2);
            //}
            //else
            //{
            //    throw new Exception("You are not a tenant");
            //}
            
        }
    }
}
