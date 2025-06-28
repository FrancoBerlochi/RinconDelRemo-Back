using System.Security.Claims;
using Application.Interfaces;
using Application.Models.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KayakController : ControllerBase
    {
        private readonly IKayakService _kayakService;
        public KayakController(IKayakService kayakService)
        {
            _kayakService = kayakService;
        }

        [HttpGet("[action]")]
        public IActionResult GetAll()
        {
            return Ok(_kayakService.GetAll());
        }

        [Authorize(Policy = "ClienteODuenio")]
        [HttpGet("Id/{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var kayak = _kayakService.GetById(id);
                return Ok(kayak);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [Authorize(Policy = "ClienteODuenio")]
        [HttpPost("[action]")]
        public IActionResult CreateKayak([FromBody] KayakCreateRequest request)
        {
            try
            {
                var obj = _kayakService.Create(request);
                return CreatedAtAction(nameof(GetById), new { id = obj.Id }, obj);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [Authorize(Policy = "DuenioKayak")]
        [HttpPut("[action]/{id}")]
        public IActionResult Update([FromRoute] int id, [FromBody] KayakUpdateRequest request)
        {
            try
            {
                _kayakService.Update(id, request);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Policy = "DuenioKayak")]
        [HttpDelete("[action]/{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            try
            {
                _kayakService.Delete(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        
        [HttpGet("[action]")]
        public IActionResult GetAvailableKayak()
        {
            try
            {
                return Ok(_kayakService.GetAvailableKayak());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [Authorize(Policy = "ClienteODuenio")]
        [HttpGet("[action]/{ownerId}")]
        public IActionResult GetKayakByOwner(string ownerId)
        {
            try
            {
                return Ok(_kayakService.GetKayakByOwner(ownerId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Policy = "ClienteODuenio")]
        [HttpPut("enable/{kayakId}")]
        public IActionResult EnableKayak(int kayakId)
        {
            try
            {
                _kayakService.EnableKayak(kayakId);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [Authorize(Policy = "ClienteODuenio")]
        [HttpPut("disable/{kayakId}")]
        public IActionResult DisableKayak(int kayakId)
        {
            try
            {
                _kayakService.DisableKayak(kayakId);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
