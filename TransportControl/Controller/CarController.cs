using Microsoft.AspNetCore.Mvc;
using TransportControl.Model.DTO;
using TransportControl.Service.Impl;
using Swashbuckle.AspNetCore.Annotations;
using TransportControl.Service;
using TransportControl.Model.Entity;

namespace TransportControl.Controller
{     
        [ApiController]
        [Route("api/[controller]")]
        public class CarController : ControllerBase
        {
            private readonly ICarService _carService;

            public CarController(ICarService carService)
            {
                _carService = carService;
            }
            [HttpGet]
            [SwaggerOperation(Summary = "Get all car")]
            [SwaggerResponse(200, "Success", typeof(CarDto))]
            [SwaggerResponse(404, "Car not found")]
            public async Task<IActionResult> GetAllCars()
            {
                var cars = await _carService.GetAllCarsAsync();
                return Ok(cars);

            }

            [HttpGet("{id}")]
            [SwaggerOperation(Summary = "Get car by ID")]
            [SwaggerResponse(200, "Success", typeof(CarDto))]
            [SwaggerResponse(404, "Car not found")]
            public async Task<IActionResult> GetById(Guid id)
            {
                var car = await _carService.GetCarByIdAsync(id);
                if (car == null) return NotFound();
                return Ok(car);
            }

            [HttpPost]
            [SwaggerOperation(Summary = "Create new car")]
            [SwaggerResponse(201, "Car created", typeof(Car))]
            public async Task<IActionResult> Create([FromBody] CreateCarDto dto)
            {
                var car = await _carService.CreateCarAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = car.Id }, car);
            }

            [HttpPut("{id}")]
            [SwaggerOperation(Summary = "Update car")]
            [SwaggerResponse(204, "Car updated")]
            [SwaggerResponse(404, "Car not found")]
            public async Task<IActionResult> Update(Guid id, [FromBody] CarDto dto)
            {
                var result = await _carService.UpdateCarAsync(id, dto);
                if (!result) return NotFound();
                return NoContent();
            }

            [HttpDelete("{id}")]
            [SwaggerOperation(Summary = "Delete car")]
            [SwaggerResponse(204, "Car deleted")]
            [SwaggerResponse(404, "Car not found")]
            public async Task<IActionResult> Delete(Guid id)
            {
                var result = await _carService.DeleteCarAsync(id);
                if (!result) return NotFound();
                return NoContent();
            }
        }
    
}
