using System.ComponentModel.DataAnnotations;

namespace ArquivoNacionalApi.Domain.Entities
{
    public class IndexPoint
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        public List<DocumentMetadata> DocumentsMetadata { get; set; } = new List<DocumentMetadata>();
    }

}
