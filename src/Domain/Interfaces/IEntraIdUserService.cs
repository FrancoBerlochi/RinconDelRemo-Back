using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IEntraIdUserService
    {
        Task DeleteUserAsync(string userId);
        Task UpdateUserAsync(string userId, string Name, string LastName);
        Task<string> CreateUserAsync(string displayName, string userPrincipalName, string password);

    }
}
