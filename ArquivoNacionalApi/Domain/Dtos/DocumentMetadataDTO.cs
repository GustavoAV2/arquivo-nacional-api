
namespace ArquivoNacionalApi.Domain.Dtos
{
    public class DocumentMetadataDTO
    {
        public Guid Id { get; set; }
        public Guid DocumentId { get; set; }
        public string Title { get; set; }
        public string Context { get; set; }
        public string SocialMarkers { get; set; }
        public List<IndexPointDTO> IndexPoints { get; set; }
        public int Points { get; set; }
    }

    public class UpdateDocumentMetadataDTO
    {
        public string? Title { get; set; }
        public string? Context { get; set; }
        public string? SocialMarkers { get; set; }
        public List<IndexPointDTO> IndexPoints { get; set; }
    }

    public class CreateDocumentMetadataDTO
    {
        public Guid Id { get; set; }
        public Guid DocumentId { get; set; }
        public Guid UserId { get; set; }
        public string? Title { get; set; }
        public string? Context { get; set; }
        public string? SocialMarkers { get; set; }
        public List<IndexPointDTO> IndexPoints { get; set; } = new List<IndexPointDTO>();
    }
}
