using FlightDocSystem.DTO;
using FlightDocSystem.Models;

namespace FlightDocSystem.Interfaces
{
    public interface IFlightService
    {
        public Task<List<Flight>> GetAllFlightAsync();
        public Task<Flight> GetFlightAsync(int id);
        public Task<int> AddFlightAsync(FlightDTO flightDTO);
        public Task UpdateFlightAsync(int id,  FlightDTO flightDTO);
        public Task DeleteFlightAsync(int id);
    }
}
