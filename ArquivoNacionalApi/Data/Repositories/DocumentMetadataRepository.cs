using ArquivoNacionalApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ArquivoNacionalApi.Data.Repositories
{
    public class DocumentMetadataRepository : Repository<DocumentMetadata>, IDocumentMetadataRepository
    {
        public DocumentMetadataRepository(DatabaseContext context) : base(context) { }

        public List<Document> GetDocumentsByUserId(Guid userId)
        {
            var notAllowDocuments = _context.DocumentMetadata.Where(d => d.UserId == userId).Select(d => d.DocumentId);
            var allowDocuments = _context.DocumentMetadata
                .Where(d => !notAllowDocuments.Contains(d.DocumentId))
                .Select(d => d.Document);
            return allowDocuments.ToList();
        }

        public DocumentMetadata GetDocumentMetadataByUserIdAndDocumentIdAsync(Guid userId, Guid documentId)
        {
            var document = _context.DocumentMetadata
                .Include(d => d.IndexPoints)
                .Include(d => d.Document)
                .AsTracking()
                .FirstOrDefault(d => d.UserId == userId && d.DocumentId == documentId);
            return document;
        }

        public async Task<List<DocumentMetadata>> GetAllDocumentMetadataAsync()
        {
            return await _dbSet.Include(d => d.Document).Include(d => d.IndexPoints).ToListAsync();
        }

        public List<DocumentMetadata> GetMetadataByDocumentId(Guid documentId)
        {
            return _context.DocumentMetadata.Where(d => d.DocumentId.Equals(documentId)).Include(d => d.IndexPoints).ToList();
        }

        public async Task UpdateMetadata(DocumentMetadata documentMetadata)
        {
            _context.Update(documentMetadata);
            await SaveChangeAsync();
        }
    }

    public interface IDocumentMetadataRepository : IRepository<DocumentMetadata>
    {
        Task<List<DocumentMetadata>> GetAllDocumentMetadataAsync();

        List<Document> GetDocumentsByUserId(Guid userId);

        DocumentMetadata GetDocumentMetadataByUserIdAndDocumentIdAsync(Guid userId, Guid documentId);

        List<DocumentMetadata> GetMetadataByDocumentId(Guid documentId);

        Task UpdateMetadata(DocumentMetadata documentMetadata);
    }
}
