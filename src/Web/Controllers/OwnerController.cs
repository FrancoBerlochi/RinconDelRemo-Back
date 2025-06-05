using Application.Interfaces;
using Application.Models.Request;
using Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OwnerController : ControllerBase
    {
        private readonly IOwnerService _ownerService;
        public OwnerController(IOwnerService ownerService)
        {
            _ownerService = ownerService;
        }

        [HttpGet("[action]")]
        public IActionResult GetAll()
        {
            return Ok(_ownerService.GetAll());
        }

        [HttpGet("id/{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var owner = _ownerService.GetById(id);
                return Ok(owner);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("NameLastname/{name}/{lastname}")]
        public IActionResult GetByNameLastname(string name, string lastname)
        {
            try
            {
                return Ok(_ownerService.GetByNameLastName(name, lastname));

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("[action]")]
        public IActionResult CreateOwner([FromBody] OwnerCreateRequest request)
        {
            try
            {
                var result = _ownerService.CreateOwner(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("[action]/{id}")]
        public IActionResult Update([FromRoute] int id, [FromBody] OwnerUpdateRequest request)
        {
            try
            {
                _ownerService.Update(id, request);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("[action]/{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            try
            {
                _ownerService.Delete(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
