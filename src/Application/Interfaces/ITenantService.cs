using Application.Models;
using Application.Models.Request;
using Domain.Entities;
using System.Security.Claims;

namespace Application.Interfaces
{
    public interface ITenantService
    {
        void Delete(string id);
        List<TenantDto> GetAll();
        TenantDto GetById(string id);
        void Update(string id, TenantUpdateRequest tenantUpdateRequest);
        Tenant Create(TenantCreateRequest tenantCreateRequest);

    }
}
