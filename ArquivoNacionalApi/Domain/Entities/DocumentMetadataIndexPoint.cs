using System.ComponentModel.DataAnnotations;

namespace ArquivoNacionalApi.Domain.Entities
{
    public class DocumentMetadataIndexPoint
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid DocumentId { get; set; }
        public Document Document { get; set; }

        [Required]
        public Guid IndexPointId { get; set; }
        public IndexPoint IndexPoint { get; set; }

    }
}
