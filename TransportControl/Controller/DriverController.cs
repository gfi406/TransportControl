using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TransportControl.Model.DTO;
using TransportControl.Model.Entity;
using TransportControl.Service;
namespace TransportControl.Controllers
{
        [ApiController]
        [Route("api/[controller]")]
        public class DriverController : ControllerBase
        {
            private readonly IDriverService _driverService;

            public DriverController(IDriverService driverService)
            {
                _driverService = driverService;
            }
        
            [HttpGet("{id}")]
            [SwaggerOperation(Summary = "Get driver by ID")]
            [SwaggerResponse(200, "Success", typeof(DriverDto))]
            [SwaggerResponse(404, "Driver not found")]
            public async Task<IActionResult> GetById(Guid id)
            {
                var driver = await _driverService.GetDriverByIdAsync(id);
                if (driver == null) return NotFound();
                return Ok(driver);
            }

            [HttpPost]
            [SwaggerOperation(Summary = "Create new driver")]
            [SwaggerResponse(201, "Driver created", typeof(Driver))]
            public async Task<IActionResult> Create([FromBody] CreateDriverDto dto)
            {
                var driver = await _driverService.CreateDriverAsync(dto);
               // return CreatedAtAction(nameof(GetById), new { id = driver.Id }, driver);
                return Ok(driver);
            }

            [HttpPut("{id}")]
            [SwaggerOperation(Summary = "Update driver")]
            [SwaggerResponse(204, "Driver updated")]
            [SwaggerResponse(404, "Driver not found")]
            public async Task<IActionResult> Update(Guid id, [FromBody] DriverDto dto)
            {
                var result = await _driverService.UpdateDriverAsync(id, dto);
                if (!result) return NotFound();
                return NoContent();
            }

            [HttpDelete("{id}")]
            [SwaggerOperation(Summary = "Delete driver")]
            [SwaggerResponse(204, "Driver deleted")]
            [SwaggerResponse(404, "Driver not found")]
            public async Task<IActionResult> Delete(Guid id)
            {
                var result = await _driverService.DeleteDriverAsync(id);
                if (!result) return NotFound();
                return NoContent();
            }
        }
    }
