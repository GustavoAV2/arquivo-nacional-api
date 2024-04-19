using System.ComponentModel.DataAnnotations;

namespace ArquivoNacionalApi.Domain.Entities
{
    public class Session
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid DocumentId { get; set; }
        public Document Document { get; set; }

        public List<User> Users { get; set; }

        public int PlayerLimit { get; set; }
    }
}
