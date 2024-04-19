using ArquivoNacionalApi.Domain.Dtos;
using ArquivoNacionalApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace ArquivoNacionalApi.Controllers 
{
    [ApiController]
    [Route("api/[controller]")]
    public class DocumentController : ControllerBase
    {
        private readonly DocumentService _documentService;

        public DocumentController(DocumentService documentService)
        {
            _documentService = documentService;
        }

        [HttpGet()]
        public async Task<ActionResult<IEnumerable<DocumentDTO>>> GetAllDocuments()
        {
            var documentDtos = await _documentService.GetAllDocumentsAsync();
            return Ok(documentDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DocumentDTO>> GetDocument(Guid id)
        {
            var documentDto = await _documentService.GetDocumentByIdAsync(id);
            if (documentDto == null)
                return NotFound();

            return Ok(documentDto);
        }

        [HttpPost()]
        public ActionResult CreateDocument([FromBody] DocumentDTO documentDto)
        {
            _documentService.CreateDocument(documentDto);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDocument(Guid id, [FromBody] DocumentDTO documentDto)
        {
            if (await _documentService.UpdateDocumentAsync(id, documentDto))
                return Ok();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDocument(Guid id)
        {
           await _documentService.DeleteDocumentAsync(id);
            return NoContent();
        }
    }
}
