using Microsoft.AspNetCore.Mvc;
using TransportControl.Model.DTO;
using TransportControl.Service;
using Swashbuckle.AspNetCore.Annotations;
using TransportControl.Model.Entity;

namespace TransportControl.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TrackPointController : ControllerBase
    {
        private readonly ITrackPointService _trackPointService;

        public TrackPointController(ITrackPointService trackPointService)
        {
            _trackPointService = trackPointService;
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Get track point by ID")]
        [SwaggerResponse(200, "Success", typeof(TrackPointDto))]
        [SwaggerResponse(404, "Track point not found")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var trackPoint = await _trackPointService.GetTrackPointByIdAsync(id);
            if (trackPoint == null) return NotFound();
            return Ok(trackPoint);
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Create new track point")]
        [SwaggerResponse(201, "Track point created", typeof(TrackPoint))]
        public async Task<IActionResult> Create([FromBody] CreateTrackPointDto dto)
        {
            var trackPoint = await _trackPointService.CreateTrackPointAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = trackPoint.Id }, trackPoint);
        }

        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Update track point")]
        [SwaggerResponse(204, "Track point updated")]
        [SwaggerResponse(404, "Track point not found")]
        public async Task<IActionResult> Update(Guid id, [FromBody] TrackPointDto dto)
        {
            var result = await _trackPointService.UpdateTrackPointAsync(id, dto);
            if (!result) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Delete track point")]
        [SwaggerResponse(204, "Track point deleted")]
        [SwaggerResponse(404, "Track point not found")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _trackPointService.DeleteTrackPointAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}