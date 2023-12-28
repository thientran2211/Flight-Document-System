using FlightDocSystem.DTO;
using FlightDocSystem.Models;

namespace FlightDocSystem.Interfaces
{
    public interface IDocumentService
    {
        public Task<List<Document>> GetAllDocumentsAsync();
        public Task<Document> GetDocumentByIdAsync(int id);
        public Task<int> AddDocumentAsync(DocumentDTO documentDTO);
        public Task<int> UploadDocumentFile(int documentId, IFormFile file);
        public Task<MemoryStream> DownloadDocumentFile(int userId, int documentId);
        public Task UpdateFileAsync(int id, IFormFile file);
        public Task DeleteFileAsync(int id);
    }
}
