using Application.Interfaces;
using Application.Models;
using Application.Models.Request;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HangerController : ControllerBase
    {
        private readonly IHangerService _hangerService;
        public HangerController(IHangerService hangerService)
        {
            _hangerService = hangerService;
        }

        [Authorize(Policy = "DuenioKayak")]
        [HttpPost("[action]")] // solo dueno
        public IActionResult Create([FromBody] HangerCreateRequest request)
        {
            try
            {
                var result = _hangerService.Create(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "admin,encargado")]
        [HttpDelete("{id}")] // solo encargado
        public IActionResult Delete(int id)
        {
            try
            {
                _hangerService.Delete(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("[action]")]
        public IActionResult GetOccupiedHangers()
        {
            return Ok(_hangerService.GetOccupiedHangers());
        }

        [HttpGet("[action]")]
        public IActionResult GetFreeHangers()
        {
            var result = _hangerService.GetFreeHangers();
            return Ok(result);
        }

        [Authorize (Policy = "ClienteODuenio")]
        [HttpGet("id/{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var hanger = _hangerService.GetById(id);
                return Ok(hanger);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [Authorize(Policy = "ClienteODuenio")]
        [HttpGet("byowner/{ownerId}")]
        public ActionResult<IEnumerable<HangerDto>> GetHangersByOwner(string ownerId)
        {
            var hangers = _hangerService.GetHangersByOwner(ownerId);
            return Ok(hangers);
        }

        [HttpGet("([action])")]
        public ActionResult<IEnumerable<HangerStatusDto>> GetAllHangerStatus()
        {
            var result = _hangerService.GetAllHangerStatus();
            return Ok(result);
        }
    }
}
