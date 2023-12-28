using FlightDocSystem.Data;
using FlightDocSystem.Interfaces;
using FlightDocSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightDocSystem.Services
{
    public class DocumentHistoryService : IDocumentHistoryService
    {
        private readonly FlightDocsContext _context;

        public DocumentHistoryService(FlightDocsContext context) 
        {
            _context = context;
        }
        public async Task<List<DocumentHistory>> GetAllDocumentHistoryAsync()
        {
            var documents = await _context.DocumentHistorys.ToListAsync();
            return documents;
        }

        public async Task<List<DocumentHistory>> GetDocumentHistoryByIdAsync(int id)
        {
            var document = await _context.DocumentHistorys.Where(d => d.Id == id).ToListAsync() ?? throw new FileNotFoundException("File Document doesn't exist in Document History");

            return document;
        }
    }
}
