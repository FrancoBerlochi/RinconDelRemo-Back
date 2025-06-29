using Application.Interfaces;
using Application.Models.Request;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdministratorController : ControllerBase
    {
        private readonly IAdministratorService _administratorService;
        public AdministratorController(IAdministratorService administratorService)
        {
            _administratorService = administratorService;
        }

        [HttpGet("[action]")]
        public IActionResult GetAll()
        {
            return Ok(_administratorService.GetAll());
        }

        [Authorize(Roles = "admin")]
        [HttpGet("Id/{id}")]
        public IActionResult GetById(string id)
        {
            try
            {
                var administrator = _administratorService.GetById(id);
                return Ok(administrator);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [Authorize(Roles = "admin")]
        [HttpPost("[action]")]
        public IActionResult CreateAdministrator([FromBody] AdministratorCreateRequest request)
        {
            try
            {
                var obj = _administratorService.Create(request);
                return Ok(obj);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [Authorize(Roles = "admin")]
        [HttpPut("[action]/{id}")]
        public IActionResult Update([FromRoute] string id, [FromBody] AdministratorUpdateRequest request)
        {
            try
            {
                _administratorService.Update(id, request);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "admin")]
        [HttpDelete("[action]/{id}")]
        public IActionResult Delete([FromRoute] string id)
        {
            try
            {
                _administratorService.Delete(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
