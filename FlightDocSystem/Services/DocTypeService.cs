using FlightDocSystem.Data;
using FlightDocSystem.DTO;
using FlightDocSystem.Interfaces;
using FlightDocSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightDocSystem.Services
{
    public class DocTypeService : IDocTypeService
    {
        private readonly FlightDocsContext _context;

        public DocTypeService(FlightDocsContext context) 
        {
            _context = context;
        }
        public async Task<int> AddDocumentTypeAsync(DocTypeDto docTypeDto)
        {
            if (docTypeDto == null)
            {
                throw new ArgumentNullException(nameof(docTypeDto));
            }

            var doctype = new DocType
            {
                DocTypeName = docTypeDto.DocTypeName,
            };

            _context.DocTypes.Add(doctype);
            await _context.SaveChangesAsync();

            return doctype.DocTypeId;
        }


        public async Task<List<DocType>> GetAllDocumentTypeAsync()
        {
            var doctypes = await _context.DocTypes.ToListAsync();
            return doctypes;
        }

        public async Task<DocType> GetDocumentTypeAsync(int id)
        {
            var doctype = await _context.DocTypes.FindAsync(id);
            if (doctype == null)
            {
                throw new Exception("Not Found document type");
            }
            return doctype;
        }

        public async Task UpdateDocumentTypeAsync(int id, DocTypeDto docTypeDto)
        {
            var existingDocType = await _context.DocTypes.FindAsync(id);
            if (existingDocType != null)
            {
                existingDocType.DocTypeName = docTypeDto.DocTypeName;

                _context.DocTypes.Update(existingDocType);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteDocumentTypeAsync(int id)
        {
            var existingDocType = _context.DocTypes.SingleOrDefault(d => d.DocTypeId == id);
            if (existingDocType != null)
            {
                _context.DocTypes.Remove(existingDocType);
                await _context.SaveChangesAsync();
            }
        }
    }
}
