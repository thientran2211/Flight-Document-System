using FlightDocSystem.Models;

namespace FlightDocSystem.Interfaces
{
    public interface IDocumentHistoryService
    {
        public Task<List<DocumentHistory>> GetAllDocumentHistoryAsync();
        public Task<List<DocumentHistory>> GetDocumentHistoryByIdAsync(int id);
    }
}
