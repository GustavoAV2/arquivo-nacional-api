using ArquivoNacionalApi.Domain.Dtos;
using ArquivoNacionalApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace ArquivoNacionalApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DocumentMetadataController : ControllerBase
    {
        private readonly IDocumentMetadataService _documentMetadataService;

        public DocumentMetadataController(IDocumentMetadataService documentMetadataService)
        {
            _documentMetadataService = documentMetadataService;
        }
        
        [EndpointDescription("Cria dados de metadata do documento, vinculado a determinado Usuario e Documento")]
        [HttpPost()]
        public ActionResult CreateDocumentMetadata([FromBody] CreateDocumentMetadataDTO documentMetadataDto)
        {
            _documentMetadataService.CreateDocumentMetadata(documentMetadataDto);
            return Ok();
        }

        [EndpointDescription("Atualiza os dados de metadata de determinado usuario registrado para o documento")]
        [HttpPut()]
        public async Task<IActionResult> UpdateDocumentMetadata([FromQuery] Guid userId, [FromQuery] Guid documentId, [FromBody] UpdateDocumentMetadataDTO documentMetadataDto)
        {
            var documentUpdated = await _documentMetadataService.UpdateDocumentMetadataAsync(userId, documentId, documentMetadataDto);
            if (documentUpdated)
            {
                return Ok();
            }
            return NoContent();
        }


        [HttpGet()]
        public async Task<ActionResult<IEnumerable<DocumentMetadataDTO>>> GetAllDocumentMetadata()
        {
            var documentMetadatas = await _documentMetadataService.GetAllDocumentMetadataAsync();
            var documentMetadataDtos = documentMetadatas.Select(dm => new DocumentMetadataDTO { 
                Id = dm.Id, 
                DocumentId = dm.DocumentId, 
                Title = dm.Title ,
                SocialMarkers = dm.SocialMarkers,
                Context = dm.Context,
                IndexPoints = dm.IndexPoints
                    .Select(i => new IndexPointDTO()
                    {
                        Id = i.Id,
                        Name = i.Name,
                    }).ToList(),
                Points = dm.Points
            });
            return Ok(documentMetadataDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DocumentMetadataDTO>> GetDocumentMetadata(Guid id)
        {
            var documentMetadata = await _documentMetadataService.GetDocumentMetadataByIdAsync(id);
            if (documentMetadata == null)
                return NotFound();

            var documentMetadataDto = new DocumentMetadataDTO { Id = documentMetadata.Id, DocumentId = documentMetadata.DocumentId, Title = documentMetadata.Title };
            return Ok(documentMetadataDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDocumentMetadata(Guid id)
        {
            var deleted = await _documentMetadataService.DeleteDocumentMetadataAsync(id);
            if (deleted)
            {
                return Ok();
            }
            return NoContent();
        }
    }
}
