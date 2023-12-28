using FlightDocSystem.Data;
using FlightDocSystem.Interfaces;
using FlightDocSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlightDocSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "System Admin, Go Office") ]
    public class DocumentHistoryController : ControllerBase
    {
        private readonly IDocumentHistoryService _documentHistory;
        private readonly FlightDocsContext _context;

        public DocumentHistoryController(IDocumentHistoryService documentHistory, FlightDocsContext context) 
        {
            _documentHistory = documentHistory;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDocumentHistory()
        {
            try
            {
                var documents = await _documentHistory.GetAllDocumentHistoryAsync();
                return Ok(documents);
            }
            catch
            {
                return NotFound("Don't found document history");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDocumentHistoryById(int id)
        {
            try
            {
                await _documentHistory.GetDocumentHistoryByIdAsync(id);
                var document = _context.DocumentHistorys.FindAsync(id);
                return Ok(document);
            }
            catch 
            {
                return NotFound("Don't found document history of this id");
            }
        }
    }
}
