using System.ComponentModel.DataAnnotations;

namespace ArquivoNacionalApi.Domain.Entities
{
    public class Document
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(1000)]
        public string FilePath { get; set; }
    }
}
