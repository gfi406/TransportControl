using Microsoft.AspNetCore.Mvc;
using TransportControl.Model.DTO;
using TransportControl.Service;
using Swashbuckle.AspNetCore.Annotations;
using TransportControl.Model.Entity;

namespace TransportControl.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TrackListController : ControllerBase
    {
        private readonly ITrackListService _trackListService;
        private readonly ITrackListDocumentationService _documentationService;

        public TrackListController(ITrackListService trackListService, ITrackListDocumentationService documentationService  )
        {
            _trackListService = trackListService;
            _documentationService = documentationService;
        }
        [HttpGet]
        [SwaggerOperation(Summary = "Get all track list ")]
        [SwaggerResponse(200, "Success", typeof(TrackListDto))]
        [SwaggerResponse(404, "Track list not found")]
        public async Task<IActionResult> GetAll()
        {
            var trackLists = await _trackListService.GetAllTrackListAsync();
            return Ok(trackLists);
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Get track list by ID")]
        [SwaggerResponse(200, "Success", typeof(TrackListDto))]
        [SwaggerResponse(404, "Track list not found")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var trackList = await _trackListService.GetTrackListByIdAsync(id);
            if (trackList == null) return NotFound();
            return Ok(trackList);
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Create new track list")]
        [SwaggerResponse(201, "Track list created", typeof(TrackList))]
        public async Task<IActionResult> Create([FromBody] CreateTrackListDto dto)
        {
            var trackList = await _trackListService.CreateTrackListAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = trackList.Id }, trackList);
        }

        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Update track list")]
        [SwaggerResponse(204, "Track list updated")]
        [SwaggerResponse(404, "Track list not found")]
        public async Task<IActionResult> Update(Guid id, [FromBody] TrackListDto dto)
        {
            var result = await _trackListService.UpdateTrackListAsync(id, dto);
            if (!result) return NotFound();
            return NoContent();
        }

        [HttpPut("/add-car/")]
        [SwaggerOperation(Summary = "Add car to track list")]
        [SwaggerResponse(200, "Car added to track list", typeof(TrackList))]
        [SwaggerResponse(400, "Car insurance expired")]
        [SwaggerResponse(404, "Track list or car not found")]
        public async Task<IActionResult> AddCarToTrackList(AddCarToTrackListDto dto)
        {
            try
            {
                var trackList = await _trackListService.AddCarToTrackListAsync(dto);
                return Ok(trackList);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex) when (ex.Message.Contains("Insurance"))
            {
               
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpPut("/add-driver/")]
        [SwaggerOperation(Summary = "Add driver to track list")]
        [SwaggerResponse(200, "Driver added to track list", typeof(TrackList))]
        [SwaggerResponse(400, "Driver insurance expired")]
        [SwaggerResponse(404, "Track list or driver not found")]
        public async Task<IActionResult> AddDriverToTrackList(AddDriverToTrackListDto dto)
        {
            try
            {
                var trackList = await _trackListService.AddDriverToTrackListAsync(dto);
                return Ok(trackList);
            }
            catch (InvalidOperationException ex) 
            {
                return BadRequest(ex.Message);
            }
            catch (KeyNotFoundException ex) 
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex) when (ex.Message.Contains("Category"))
            {

                return StatusCode(500, "Internal server error");
            }
        }
        [HttpPut("/close-list/")]
        [SwaggerOperation(Summary = "Close list")]
        [SwaggerResponse(200, "List closed", typeof(TrackList))]
        [SwaggerResponse(400, "List not close")]
        [SwaggerResponse(404, "Not found")]
        public async Task<IActionResult> TrackListClose(TrackListCloseDto dto)
        {
            try
            {
                var trackList = await _trackListService.TrackListCloseAsync(dto);
                return Ok(trackList);
            }
            catch (InvalidOperationException ex) 
            {
                return BadRequest(ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex) when (ex.Message.Contains("Category"))
            {
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpGet("{id}/documentation")]
        [SwaggerOperation(Summary = "Generate documentation for track list")]
        [SwaggerResponse(200, "Documentation generated", typeof(string))]
        [SwaggerResponse(404, "Track list not found")]
        public async Task<IActionResult> GenerateDocumentation(Guid id,
            [FromQuery] string templatePath = "Templates/form.xlsx",
            [FromQuery] string outputFolder = "Documents/TrackLists")
        {
            var trackList = await _trackListService.GetTrackListByIdAsync(id);
            if (trackList == null) return NotFound();

            try
            {
                var timestamp = DateTime.UtcNow.ToString("yyyyMMdd_HHmmss");
                var outputExcelPath = Path.Combine(outputFolder, $"TrackList_{id}_{timestamp}.xlsx");
                var outputPdfPath = Path.Combine(outputFolder, $"TrackList_{id}_{timestamp}.pdf");

                // Генерируем документацию
                var generatedPdfPath = await _documentationService.GenerateTrackListDocumentationAsync(
                    id,
                    templatePath,
                    outputExcelPath,
                    outputPdfPath
                );


                var fileBytes = await System.IO.File.ReadAllBytesAsync(generatedPdfPath);
                var contentType = "application/pdf";
                var fileName = Path.GetFileName(generatedPdfPath);

                return File(fileBytes, contentType, fileName);
            }
            catch (Exception ex)
            {
                
                return StatusCode(500, new
                {
                    message = "Ошибка при генерации документации",
                    error = ex.Message
                });
            }
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Delete track list")]
        [SwaggerResponse(204, "Track list deleted")]
        [SwaggerResponse(404, "Track list not found")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _trackListService.DeleteTrackListAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}