using FlightDocSystem.Data;
using FlightDocSystem.DTO;
using FlightDocSystem.Interfaces;
using Microsoft.EntityFrameworkCore;
using FlightDocSystem.Models;

namespace FlightDocSystem.Services
{
    public class DocumentService : IDocumentService
    {
        private readonly FlightDocsContext _context;

        public DocumentService(FlightDocsContext context)
        {
            _context = context;
        }

        public async Task<int> AddDocumentAsync(DocumentDTO documentDTO)
        {
            var document = new Document
            {
                Version = documentDTO.Version,
                Note = documentDTO.Note,
                CreatedDate = DateTime.Now,
                GroupId = documentDTO.GroupId,
                FlightId = documentDTO.FlightID,
                DocTypeId = documentDTO.DocTypeId,
                UserId = documentDTO.UserId
            };

            _context.Documents.Add(document);
            await _context.SaveChangesAsync();

            return document.DocumentID;
        }

        public async Task<MemoryStream> DownloadDocumentFile(int userId, int documentId)
        {
            var document = await _context.Documents.FirstOrDefaultAsync(d => d.DocumentID == documentId) ?? throw new FileNotFoundException();

            if (string.IsNullOrEmpty(document.File))
            {
                throw new FileNotFoundException("File data not found in the database.");
            }

            byte[] fileData = Convert.FromBase64String(document.File);
            var stream = new MemoryStream(fileData);

            return stream;
        }
 
        public async Task<List<Document>> GetAllDocumentsAsync()
        {
            var documents = await _context.Documents.ToListAsync();
            return documents;
        }

        public async Task<Models.Document> GetDocumentByIdAsync(int id)
        {
            var document = await _context.Documents
                .Include(d => d.Flight)
                .FirstOrDefaultAsync(d => d.DocumentID == id);

            if (document == null)
            {
                throw new FileNotFoundException();
            }

            return document;
        }


        public async Task<int> UploadDocumentFile(int documentId, IFormFile file)
        {
            var existingDocument = await _context.Documents
            .Include(d => d.Flight)
                .FirstOrDefaultAsync(d => d.DocumentID == documentId) ?? throw new FileNotFoundException();

            if (file != null)
            {
                var uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");
                var filePath = Path.Combine(uploadFolder, file.FileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }

                existingDocument.File = filePath;
                existingDocument.DocumentName = Path.GetFileName(file.FileName);
                await _context.SaveChangesAsync();
            }

            return documentId;
        }

        public async Task UpdateFileAsync(int id, IFormFile file)
        {
            var existingDocument = await _context.Documents.Include(d => d.Flight).Include(d => d.User)
                                                .FirstOrDefaultAsync(d => d.DocumentID == id) ?? throw new FileNotFoundException();

            var uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");
            var filePath = Path.Combine(uploadFolder, file.FileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            existingDocument.Version += 0.1m;
            existingDocument.File = filePath;
            existingDocument.UpdatedDate = DateTime.Now;
            existingDocument.DocumentName = Path.GetFileName(file.FileName);

            await SaveDocumentHistory(existingDocument);
            await _context.SaveChangesAsync();

        }

        public async Task DeleteFileAsync(int id)
        {
            var existingDocument = _context.Documents.SingleOrDefault(d => d.DocumentID == id);
            if (existingDocument != null )
            {
                _context.Documents.Remove(existingDocument);
                await _context.SaveChangesAsync();
            }
        }

        private async Task<string> SaveFileToString(IFormFile file)
        {
            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                byte[] fileData = memoryStream.ToArray();
                return Convert.ToBase64String(fileData);
            }
        }

        private async Task SaveDocumentHistory(Document document)
        {
            try
            {
                var documentHistory = new DocumentHistory
                {
                    Name = document.DocumentName,
                    UpdateDate = DateTime.Now,
                    Version = (decimal)document.Version,
                    DocumentID = document.DocumentID,      
                };
                _context.DocumentHistorys.Add(documentHistory);
                await _context.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                ex.Message.ToString();
            }
        }
    }
}
