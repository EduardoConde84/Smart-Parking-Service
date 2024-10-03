using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using SmartParkingLot.Domain.Dtos;
using SmartParkingLot.Domain.Helpers.Pagination;
using SmartParkingLot.Domain.Interfaces;
using SmartParkingLot.Domain.Models;

namespace SmartParkingLot.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class ParkingSpotsController(
        ILogger<ParkingSpotsController> logger, 
        IParkingSpotsService service, 
        IDevicesService devicesService, 
        IPaginationHelper paginationHelper) : Controller
    {
        private readonly IParkingSpotsService _service = service;
        private readonly IDevicesService _devicesService = devicesService;

        private readonly IPaginationHelper _paginationHelper = paginationHelper;

        private readonly ILogger<ParkingSpotsController> _logger = logger;

        [HttpPost("{id:int}/occupy")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> OccupySpot(int id, [FromBody] DevicesDto devices)
        {
            if (!ModelState.IsValid)
                return BadRequest("Model not valid");

            var devicesResult = await _devicesService.GetDeviceRegistered(devices);
            if (devicesResult.IsFailure) return BadRequest(devicesResult.Error);

            var parkingSpotDto = new ParkingSpotDto() { Id = id, IsAvailable = false };
            var result = await _service.ManageSpotAsync(id, parkingSpotDto);
            if (result.IsFailure)
            {
                _logger.LogInformation("Spot Not Occupied");
                return BadRequest(result.Error);
            }
            return Ok($"Registered device number {devices.DeviceAsignedNumber} successfully occupied an spot");
        }

        [HttpPost("{id:int}/free")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> FreeSpot(int id, [FromBody] DevicesDto devices)
        {
            if (!ModelState.IsValid)
                return BadRequest("Model not valid");

            var devicesResult = await _devicesService.GetDeviceRegistered(devices);
            if (devicesResult.IsFailure) return BadRequest(devicesResult.Error);

            var parkingSpotDto = new ParkingSpotDto() { Id = id, IsAvailable = true };
            var result = await _service.ManageSpotAsync(id, parkingSpotDto);
            if (result.IsFailure)
            {
                _logger.LogInformation("Spot Not Freed");
                return BadRequest(result.Error);
            }

            return Ok($"Registered device number {devices.DeviceAsignedNumber} successfully liberated an spot");
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ParkingSpotDto>), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetAllParkingSpots([FromQuery] PaginationFilterRequest filter)
        {
            var result = await _service.GetAllParkingSpotsAsync();

            if (result.Any()) 
            {
                var pagedResponse = _paginationHelper.CreatePagedResponse(
                    result,
                    filter.PageNumber,
                    filter.PageSize);

                var metadata = new
                {
                    pagedResponse.TotalCount,
                    pagedResponse.PageSize,
                    pagedResponse.CurrentPage,
                    pagedResponse.TotalPages,
                    pagedResponse.HasNext,
                    pagedResponse.HasPrevious
                };

                Response.Headers.Append("X-Pagination", JsonSerializer.Serialize(metadata));
                
                return Ok(pagedResponse.Data);         
            }

            _logger.LogInformation("Parking Spots not found");
            return NotFound("Products not found");
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateParkingSpot()
        {
            var result = await _service.CreateSpotAsync();

            if (result) return Ok("Parking spot created");

            _logger.LogInformation("Error: Parking spot not created");
            return BadRequest("Error: Parking spot not created");
        }

        [HttpDelete("DeleteProduct/{id:int}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> DeleteParkingSpot(int id)
        {
            var result = await _service.DeleteSpotAsync(id);

            if (result.IsFailure)
            {
                _logger.LogInformation("Parking spot with {id} not deleted", id);
                return BadRequest($"Parking spot with {id} number not deleted");
            }
            return Ok($"Parking spot with {id} deleted with success");
        }

    }
}