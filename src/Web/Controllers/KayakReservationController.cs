using Application.Interfaces;
using Application.Models;
using Application.Models.Request;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KayakReservationController : ControllerBase
    {
        private readonly IKayakReservationService _kayakReservationService;
        public KayakReservationController(IKayakReservationService kayakReservationService)
        {
            _kayakReservationService = kayakReservationService;
        }

        
        [HttpGet("[action]")]
        public IActionResult GetAll()
        {
            return Ok(_kayakReservationService.GetAll());
        }

        
        [HttpGet("Id/{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var reservationKayak = _kayakReservationService.GetById(id);
                return Ok(reservationKayak);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost("[action]")]
        public IActionResult CreateReservationKayak([FromBody] KayakReservationCreateRequest request)
        {
            var obj = _kayakReservationService.Create(request);
            
            return Ok(obj);
        }

       
        [HttpPut("[action]/{id}")]
        public IActionResult Update([FromRoute] int id, [FromBody] KayakReservationUpdateRequest request)
        {
            try
            {
                _kayakReservationService.Update(id, request);
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
                _kayakReservationService.Delete(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("canceled/{id}")]
        public IActionResult CanceledReservation([FromRoute] int id)
        {
            try
            {
                _kayakReservationService.CanceledReservation(id);
                return Ok("Reserva cancelada exitosamente.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("activas")]
        public ActionResult<List<KayakReservationDto>> GetActiveReservations()
        {
            try
            {
                var reservas = _kayakReservationService.GetActiveReservations();
                return Ok(reservas);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpGet("canceladas")]
        public ActionResult<List<KayakReservationDto>> GetCancelledReservations()
        {
            try
            {
                var reservas = _kayakReservationService.GetCancelledReservations();
                return Ok(reservas);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpGet("finalizadas")]
        public ActionResult<List<KayakReservationDto>> GetCompletedReservations()
        {
            try
            {
                var reservas = _kayakReservationService.GetCompletedReservations();
                return Ok(reservas);
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpGet]
        public ActionResult<List<KayakReservationDto>> GetReservations([FromQuery] DateTime? date, [FromQuery] string? tenantId)
        {
            try
            {
                var reservations = _kayakReservationService.GetReservations(date, tenantId);
                return Ok(reservations);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
