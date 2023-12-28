using FlightDocSystem.Data;
using FlightDocSystem.DTO;
using FlightDocSystem.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlightDocSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightController : ControllerBase
    {
        private readonly IFlightService _flightService;
        private readonly FlightDocsContext _context;

        public FlightController(IFlightService flightService, FlightDocsContext context) 
        {
            _flightService = flightService;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllFlights()
        {
            try
            {
                var flights = await _flightService.GetAllFlightAsync();
                return Ok(flights);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFlightById(int id)
        {
            var flight = await _flightService.GetFlightAsync(id);
            return flight != null ? Ok(flight) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> AddNewFlight(FlightDTO flightDTO)
        {
            try
            {
                var newFlight = await _flightService.AddFlightAsync(flightDTO);
                var flight = await _flightService.GetFlightAsync(newFlight);
                return flight != null ? Ok(flight) : NotFound();
            }
            catch 
            {
                return BadRequest();
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateFlight(int id, FlightDTO flightDTO)
        {
            var existingFlight = await _context.Flights.FindAsync(id);
            if (existingFlight != null)
            {
                await _flightService.UpdateFlightAsync(id, flightDTO);
                return Ok(existingFlight);
            }

            return NotFound();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteFlight(int id)
        {
            var existingFlight = _context.Flights.SingleOrDefault(f => f.FlightID == id);
            if (existingFlight != null)
            {
                await _flightService.DeleteFlightAsync(id);
                return Ok("Delete flight suceeded!");
            }

            return NotFound();
        }
    }
}
