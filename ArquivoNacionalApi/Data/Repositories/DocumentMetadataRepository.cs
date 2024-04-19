using ArquivoNacionalApi.Domain.Entities;

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
            var document = _context.DocumentMetadata.FirstOrDefault(d => d.UserId == userId && d.DocumentId == documentId);
            return document;
        }
    }

    public interface IDocumentMetadataRepository : IRepository<DocumentMetadata>
    {
        List<Document> GetDocumentsByUserId(Guid userId);

        DocumentMetadata GetDocumentMetadataByUserIdAndDocumentIdAsync(Guid userId, Guid documentId);
    }
}
