using Application.Models;
using Application.Models.Request;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface ITenantService
    {
        void Delete(int id);
        List<TenantDto> GetAll();
        TenantDto GetById(int id);
        void Update(int id, TenantUpdateRequest tenantUpdateRequest);
        Tenant Create(TenantCreateRequest tenantCreateRequest);

    }
}
