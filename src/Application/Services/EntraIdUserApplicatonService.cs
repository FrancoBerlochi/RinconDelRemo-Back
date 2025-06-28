using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;

namespace Application.Services
{
    public class EntraIdUserApplicationService
    {
        private readonly IEntraIdUserService _entraIdUserService;

        public EntraIdUserApplicationService(IEntraIdUserService entraIdUserService)
        {
            _entraIdUserService = entraIdUserService;
        }

        public async Task DeleteUserAsync(string userId)
        {
            // Aquí puedes añadir lógica de validación o negocio específica de la aplicación
            if (string.IsNullOrWhiteSpace(userId))
            {
                throw new ArgumentException("User ID cannot be null or empty.", nameof(userId));
            }
            await _entraIdUserService.DeleteUserAsync(userId);
        }

        public async Task UpdateUserAsync(string userId, string Name, string LastName)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                throw new ArgumentException("User ID cannot be null or empty.", nameof(userId));
            }
            // Más validaciones si es necesario
            await _entraIdUserService.UpdateUserAsync(userId, Name, LastName);
        }
        public async Task<string> CreateUserAsync(string displayName, string userPrincipalName, string password)
        {
            // Lógica de validación y negocio para la creación de usuario
            if (string.IsNullOrWhiteSpace(displayName) || string.IsNullOrWhiteSpace(userPrincipalName) || string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException("All fields are required.");
            }
            return await _entraIdUserService.CreateUserAsync(displayName, userPrincipalName, password);
        }
    }
}
