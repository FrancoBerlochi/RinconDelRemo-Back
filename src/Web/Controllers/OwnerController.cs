using Application.Interfaces;
using Application.Models.Request;
using Domain.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;

namespace Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OwnerController : ControllerBase
    {
        private readonly IOwnerService _ownerService;
        public OwnerController(IOwnerService ownerService)
        {
            _ownerService = ownerService;
        }

        [Authorize (Policy = "Cliente")]
        [HttpGet ("chau")]
        public IActionResult Geta()
        {
            return Ok("a");
        }

        [Authorize (Policy = "DuenioKayak")]
        [HttpGet("chaussss")]
        public IActionResult Getb()
        {
            var usuario = User;
            return Ok(usuario);
        }

        [Authorize (Roles = "admin")]
        [HttpGet("hola")]
        public IActionResult Getc() 
        {                   
           return Ok("User");
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
