using FlightDocSystem.DTO;
using FlightDocSystem.Models;

namespace FlightDocSystem.Interfaces
{
    public interface IDocTypeService
    {
        public Task<List<DocType>> GetAllDocumentTypeAsync();
        public Task<DocType> GetDocumentTypeAsync(int id);
        public Task<int> AddDocumentTypeAsync(DocTypeDto docTypeDto);
        public Task UpdateDocumentTypeAsync(int id, DocTypeDto docTypeDto);
        public Task DeleteDocumentTypeAsync(int id);
    }
}
