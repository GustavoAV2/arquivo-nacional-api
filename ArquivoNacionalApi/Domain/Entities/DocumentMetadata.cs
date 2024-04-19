using System.ComponentModel.DataAnnotations;

namespace ArquivoNacionalApi.Domain.Entities
{
    public class DocumentMetadata
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid DocumentId { get; set; }
        public Document Document { get; set; }

        [Required]
        public Guid UserId { get; set; }
        public User User { get; set; }

        public List<IndexPoint> IndexPoints { get; set; } = new List<IndexPoint>();

        [MaxLength(300)]
        public string Title { get; set; }

        [MaxLength(4000)]
        public string Context { get; set; }

        public string SocialMarkers { get; set; }

        public int Points { get; set; } = 0;

        public List<string> GetListSocialMakers()
        {
            return SocialMarkers.Split(',').ToList();
        }
    }
}
