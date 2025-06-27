using Application.Interfaces;
using Application.Models.Request;
using Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TenantController : ControllerBase
    {
        private readonly ITenantService _tenantService;
        public TenantController (ITenantService tenantService)
        {
            _tenantService = tenantService;
        }

        [HttpGet("[action]")]
        public IActionResult GetAll()
        {
            var tenant = _tenantService.GetAll();
            return Ok(tenant);
        }

        [Authorize (Policy = "Cliente")]
        [HttpGet("Id/{id}")]
        public IActionResult GetById(string id)
        {
            try
            {
                var tenant = _tenantService.GetById(id);
                return Ok(tenant);
            }
            catch (Exception ex) 
            { 
                return NotFound(ex.Message);
            }
        }

        [HttpPost("[action]")]
        public IActionResult CreateTenant([FromBody] TenantCreateRequest request)
        {
            try
            {
                _tenantService.Create(request);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Policy = "Cliente")]
        [HttpPut("[action]/{id}")]
        public IActionResult Update([FromRoute] string id, [FromBody] TenantUpdateRequest request)
        {
            try
            {
                _tenantService.Update(id, request);
                return NoContent();
            }
            catch (Exception ex)
            { 
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Policy = "Cliente")]
        [HttpDelete("[action]/{id}")]
        public IActionResult Delete([FromRoute] string id)
        {
            try
            {
                _tenantService.Delete(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
