using FlightDocSystem.Data;
using FlightDocSystem.DTO;
using FlightDocSystem.Interfaces;
using FlightDocSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightDocSystem.Services
{
    public class FlightService : IFlightService
    {
        private readonly FlightDocsContext _context;

        public FlightService(FlightDocsContext context) 
        {
            _context = context;
        }

        public async Task<List<Flight>> GetAllFlightAsync()
        {
            var flights = await _context.Flights.ToListAsync();
            return flights;
        }

        public async Task<Flight> GetFlightAsync(int id)
        {           
            var flight = await _context.Flights.FindAsync(id);

            if (flight == null)
            {
                throw new Exception("Not found Flights");
            }

            return flight;
        }

        public async Task<int> AddFlightAsync(FlightDTO flightDTO)
        {
            var flight = new Flight
            {
                FlightNo = flightDTO.FlightNo,
                CreateDate = DateTime.Now,
                DepartureTime = flightDTO.DepartureTime,
                PointOfLoading = flightDTO.PointOfLoading,
                PointOfUnloading = flightDTO.PointOfUnloading,
                Route = flightDTO.Route,
            };
            _context.Flights.Add(flight);
            await _context.SaveChangesAsync();

            return flight.FlightID;
        }

        public async Task UpdateFlightAsync(int id, FlightDTO flightDTO)
        {
            var existingFlight = await _context.Flights.FindAsync(id);
            if (existingFlight != null)
            {
                existingFlight.FlightNo = flightDTO.FlightNo;
                existingFlight.CreateDate = DateTime.Now;
                existingFlight.DepartureTime = flightDTO.DepartureTime;
                existingFlight.PointOfLoading = flightDTO.PointOfLoading;
                existingFlight.PointOfUnloading = flightDTO.PointOfUnloading;
                existingFlight.Route = flightDTO.Route;
            }
            _context.Flights.Update(existingFlight);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteFlightAsync(int id)
        {
            var existingFlight = _context.Flights.SingleOrDefault(f => f.FlightID == id);
            if (existingFlight != null)
            {
                _context.Flights.Remove(existingFlight);
                await _context.SaveChangesAsync();
            }
        }
    }
}
