using FlightDocSystem.Data;
using FlightDocSystem.DTO;
using FlightDocSystem.Interfaces;
using FlightDocSystem.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlightDocSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        private readonly IDocumentService _documentService;
        private readonly FlightDocsContext _context;

        public DocumentController(IDocumentService documentService, FlightDocsContext context) 
        {
            _documentService = documentService;
            _context = context;
        }

        [HttpGet("GetAllDocument")]
        public async Task<IActionResult> GetAllDocument()
        {
            try
            {
                var documents = await _documentService.GetAllDocumentsAsync();
                return Ok(documents);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("GetDocumentById")]
        public async Task<IActionResult> GetDocumentById(int id)
        {
            try
            {
                var document = await _documentService.GetDocumentByIdAsync(id);
                return Ok(document);
            }
            catch
            { 
                return NotFound();
            }
        }

        [HttpPost("AddNewDocument")]
        public async Task<IActionResult> AddNewDocument(DocumentDTO documentDTO)
        {
            try
            {
                await _documentService.AddDocumentAsync(documentDTO);
                return Ok("Add New Document Succeeded!");
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost("Upload-DocumentFile")]
        public async Task<IActionResult> UploadFileToDocument(int documentId, IFormFile file)
        {
            try
            {
                await _documentService.UploadDocumentFile(documentId, file);
                return Ok("Upload Document succeeded!");
            }
            catch 
            {
                return BadRequest();
            }             
        }

        [HttpGet("Download-DocumentFile")]
        public async Task<IActionResult> DownloadFile(int userId, int documentId)
        {
            try
            {
                var document = await _documentService.GetDocumentByIdAsync(documentId);
                var stream = await _documentService.DownloadDocumentFile(userId, documentId);

                Response.Headers.Add("Content-Disposition", $"attachment; filename={document.DocumentName}");
                return File(stream, "application/pdf");
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut("Update-DocumentFile")]
        public async Task<IActionResult> UpdateFile(int documentId, IFormFile file)
        {
            try
            {
                var document = await _documentService.GetDocumentByIdAsync(documentId);
                await _documentService.UpdateFileAsync(documentId, file);
                return Ok(document);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpDelete("Delete-DocumentFile")]
        public async Task<IActionResult> DeleteDocument(int documentId)
        {
            var existingDocument = _context.Documents.SingleOrDefault(d => d.DocumentID == documentId); 
            if (existingDocument != null)
            {
                await _documentService.DeleteFileAsync(documentId);
                return Ok("Delete Document succeeded!!!");
            }

            return NotFound();
        }
    }
}
