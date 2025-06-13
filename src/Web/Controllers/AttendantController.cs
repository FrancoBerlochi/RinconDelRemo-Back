using Application.Interfaces;
using Application.Models;
using Application.Models.Request;
using Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendantController : ControllerBase
    {
        private readonly IAttendantService _attendantService;
        public AttendantController(IAttendantService attendantService)
        {
            _attendantService = attendantService;
        }

        [HttpGet("[action]")]
        public IActionResult GetAll()
        {
            return Ok(_attendantService.GetAll());
        }

        [HttpGet("Id/{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var attendant = _attendantService.GetById(id);
                return Ok(attendant);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost("[action]")]
        public ActionResult<AttendantDto> CreateAttendant([FromBody] AttendantCreateRequest request)
        {
            try
            {
                var result = _attendantService.Create(request);
                return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut("[action]/{id}")]
        public IActionResult UpdateAttendant(int id, [FromBody] AttendantUpdateRequest request)
        {
            try
            {
                _attendantService.Update(id, request);
                return NoContent();
            }
            catch (NotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("[action]/{id}")]
        public IActionResult DeleteAttendant(int id)
        {
            try
            {
                _attendantService.Delete(id);
                return NoContent();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost("checkin/{id}")]
        public IActionResult CheckIn(int id)
        {
            try
            {
                _attendantService.CheckIn(id);
                return Ok("Check-in realizado con éxito");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("checkout/{id}")]
        public IActionResult CheckOut(int id)
        {
            try
            {
                _attendantService.CheckOut(id);
                return Ok("Check-out realizado con éxito");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
