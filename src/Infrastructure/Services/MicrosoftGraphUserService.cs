using Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Graph;
using Azure.Identity;
using Microsoft.Graph.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class MicrosoftGraphUserService : IEntraIdUserService
    {
        private readonly GraphServiceClient _graphClient;

        public MicrosoftGraphUserService(IConfiguration configuration)
        {
            // Configuración del GraphServiceClient (como lo vimos antes)
            var tenantId = configuration["AzureAd:TenantId"];
            var clientId = configuration["AzureAd:ClientId"];
            var clientSecret = configuration["AzureAd:ClientSecret"];

            var options = new ClientSecretCredentialOptions
            {
                AuthorityHost = AzureAuthorityHosts.AzurePublicCloud
            };

            var clientSecretCredential = new ClientSecretCredential(
                tenantId, clientId, clientSecret, options);

            _graphClient = new GraphServiceClient(clientSecretCredential);
        }

        // VOY A RELLENAR CADA MÉTODO AHORA:
        public async Task DeleteUserAsync(string userId)
        {
            try
            {
                await _graphClient.Users[userId].DeleteAsync();
            }
            catch (Microsoft.Graph.Models.ODataErrors.ODataError ex) // <--- ¡EL BREAKPOINT DEBE ESTAR AQUÍ!
            {
                // Cuando la ejecución se detenga aquí, inspecciona el objeto 'ex'
                Console.WriteLine($"Error de Graph al eliminar usuario {userId}:");
                Console.WriteLine($"  Código de error: {ex.Error?.Code}"); // <-- ¡Este es el valor que necesitamos!
                Console.WriteLine($"  Mensaje: {ex.Error?.Message}");     // <-- ¡Este es el valor que necesitamos!
                Console.WriteLine($"  HTTP Status: {ex.ResponseStatusCode}");
                throw new ApplicationException($"Error al eliminar usuario en Entra ID: {ex.Error?.Message ?? ex.Message}", ex);
            }
        }
        public async Task UpdateUserAsync(string userId, string displayName, string jobTitle)
        {
            var userToUpdate = new User
            {
                DisplayName = displayName,
                JobTitle = jobTitle
            };
            await _graphClient.Users[userId].PatchAsync(userToUpdate);
        }
        public async Task<string> CreateUserAsync(string displayName, string userPrincipalName, string password)
        {
            var newUser = new User
            {
                AccountEnabled = true,
                DisplayName = displayName,
                MailNickname = userPrincipalName.Split('@')[0], // Generalmente es la parte antes del @
                UserPrincipalName = userPrincipalName,
                PasswordProfile = new PasswordProfile
                {
                    ForceChangePasswordNextSignIn = true,
                    Password = password
                }
            };
            var createdUser = await _graphClient.Users.PostAsync(newUser);
            return createdUser.Id;
        }
    }
}
