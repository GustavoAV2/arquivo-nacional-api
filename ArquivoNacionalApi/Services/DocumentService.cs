using ArquivoNacionalApi.Data.Repositories;
using ArquivoNacionalApi.Domain.Dtos;
using ArquivoNacionalApi.Domain.Entities;

namespace ArquivoNacionalApi.Services
{
    public class DocumentService : IDocumentService
    {
        private readonly IRepository<Document> _documentRepository;

        public DocumentService(IRepository<Document> documentRepository)
        {
            _documentRepository = documentRepository;
        }

        public async Task<IEnumerable<DocumentDTO>> GetAllDocumentsAsync()
        {
            var documents = await _documentRepository.GetAllAsync();
            return documents.Select(d => new DocumentDTO { Id = d.Id, FilePath = d.FilePath });
        }

        public async Task<DocumentDTO> GetDocumentByIdAsync(Guid id)
        {
            var document = await _documentRepository.GetByIdAsync(id);
            return new DocumentDTO { Id = document.Id, FilePath = document.FilePath };
        }

        public void CreateDocument(DocumentDTO documentDto)
        {
            var newDocument = new Document() { Id = Guid.NewGuid(), FilePath = documentDto.FilePath };
            _documentRepository.Add(newDocument);
        }

        public async Task<bool> UpdateDocumentAsync(Guid id, DocumentDTO documentDto)
        {
            var document = await _documentRepository.GetByIdAsync(id);
            if (document != null)
            {
                var updatedDocument = new Document() { Id = document.Id, FilePath = documentDto.FilePath };
                _documentRepository.Update(updatedDocument);
                return true;
            }
            return false;
        }

        public async Task DeleteDocumentAsync(Guid id)
        {
            var document = await _documentRepository.GetByIdAsync(id);
            if (document != null)
            {
                _documentRepository.Delete(document);
            }
        }
    }

    public interface IDocumentService
    {
        Task<IEnumerable<DocumentDTO>> GetAllDocumentsAsync();
        Task<DocumentDTO> GetDocumentByIdAsync(Guid id);
        void CreateDocument(DocumentDTO documentDto);
        Task<bool> UpdateDocumentAsync(Guid id, DocumentDTO documentDto);
        Task DeleteDocumentAsync(Guid id);
    }
}
