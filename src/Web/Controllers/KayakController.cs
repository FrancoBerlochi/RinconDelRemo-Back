using Application.Interfaces;
using Application.Models.Request;
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

        [HttpPost("[action]")]
        public IActionResult CreateKayak([FromBody] KayakCreateRequest request)
        {
            try
            {
                var result = _kayakService.Create(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

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
            return Ok(_kayakService.GetAvailableKayak());
        }
    }
}
