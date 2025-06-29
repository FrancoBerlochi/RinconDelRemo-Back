using Application.Models.Request;
using Application.Services;
using Domain.Interfaces;
using Infrastructure.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Graph.Models;
using Microsoft.Graph.Models.ODataErrors;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EntraIdUserController : Controller
    {
        private readonly EntraIdUserApplicationService _applicationService;
        private readonly IEntraIdUserService _entraIdUserService;

        public EntraIdUserController(EntraIdUserApplicationService applicationService, IEntraIdUserService entraIdUserService)
        {
            _applicationService = applicationService;
            _entraIdUserService = entraIdUserService;

        }

        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            try
            {
                await _applicationService.DeleteUserAsync(userId); // O await _mediator.Send(new DeleteEntraIdUserCommand(userId));
                return Ok($"Usuario {userId} eliminado exitosamente.");
            }
            // Puedes capturar excepciones específicas aquí si las relanzas desde tu servicio de aplicación/infraestructura
            catch (ApplicationException ex) // Si relanzas una excepción personalizada desde Infrastructure
            {
                // Aquí podrías inspeccionar ex.InnerException si necesitas más detalles del error de Graph
                // Por ejemplo, si el inner exception es un ODataError y tiene un código específico
                if (ex.InnerException is ODataError odataError)
                {
                    if (odataError.Error?.Code == "Request_ResourceNotFound")
                    {
                        return NotFound($"Usuario con ID {userId} no encontrado en Entra ID. Detalle: {odataError.Error?.Message}");
                    }
                    if (odataError.Error?.Code == "Authorization_RequestDenied")
                    {
                        return StatusCode(403, $"No tienes permisos para eliminar este usuario. Detalle: {odataError.Error?.Message}");
                    }
                    // Puedes añadir más casos para otros códigos de error de Graph
                }
                return StatusCode(500, $"Error al eliminar usuario: {ex.Message}");
            }
            catch (Exception ex) // Para cualquier otra excepción inesperada
            {
                return StatusCode(500, $"Error inesperado al eliminar usuario: {ex.Message}");
            }
        }
        [HttpPatch("{userId}")]
        public async Task<IActionResult> UpdateUser(string userId, [FromBody] TenantUpdateRequest request)
        {
            try
            {
                await _applicationService.UpdateUserAsync(userId, request.Name, request.LastName);
                return Ok($"Usuario {userId} actualizado exitosamente.");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al actualizar usuario: {ex.Message}");
            }
        }

        [HttpPost("{userId}")]
        public async Task<IActionResult> RoleAssigment(string userId, string appObjectId, string appRoleId) 
        {
            try
            {
                await _applicationService.AppRoleToUserAsync(userId, appObjectId, appRoleId);
                return Ok($"Usuario {userId} actualizado exitosamente.");

            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al actualizar el rol del usuario: {ex.Message}");
            }

        }

        [HttpGet("users")]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
               
                var  users = await _entraIdUserService.GetAllUsersAsync();
                return Ok((users.Select(u => new
                {
                    u.Id,
                    u.DisplayName,
                    u.UserPrincipalName,
                    u.Mail
                })));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al actualizar el rol del usuario: {ex.Message}");
            }
            
        }

    }
}