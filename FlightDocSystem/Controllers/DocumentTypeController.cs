using FlightDocSystem.Data;
using FlightDocSystem.DTO;
using FlightDocSystem.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlightDocSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentTypeController : ControllerBase
    {
        private readonly IDocTypeService _docType;
        private readonly FlightDocsContext _context;

        public DocumentTypeController(IDocTypeService docType, FlightDocsContext context) 
        {
            _docType = docType;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDocumentType()
        {
            try
            {
                var doctypes = await _docType.GetAllDocumentTypeAsync();
                return Ok(doctypes);
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDocumentTypeById(int id)
        {
            try
            {
                var doctype = _docType.GetDocumentTypeAsync(id);
                return Ok(doctype);
            }
            catch { return NotFound(); }
        }

        [HttpPost]
        public async Task<IActionResult> AddNewDocumentType(DocTypeDto docTypeDto)
        {
            try
            {
                var newDocType = await _docType.AddDocumentTypeAsync(docTypeDto);
                var doctype = await _docType.GetDocumentTypeAsync(newDocType);
                return doctype != null ? Ok(doctype) : NotFound();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateDocumentType(int id, DocTypeDto docTypeDto)
        {
            var existingDocumentType = await _context.DocTypes.FindAsync(id);
            if (existingDocumentType != null)
            {
                await _docType.UpdateDocumentTypeAsync(id, docTypeDto);
                return Ok(existingDocumentType);
            }
            return BadRequest();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteDocumentType(int id)
        {
            var existingDocumentType = _context.DocTypes.SingleOrDefault(d => d.DocTypeId == id);
            if (existingDocumentType != null)
            {
                await _docType.DeleteDocumentTypeAsync(id);
                return Ok();
            }
            return NotFound();
        }
    }
}
