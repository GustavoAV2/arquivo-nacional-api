using System.ComponentModel.DataAnnotations;

namespace ArquivoNacionalApi.Domain.Entities;

public class User
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [MaxLength(255)]
    public string Name { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [MaxLength(255)]
    public string Password { get; set; }

    [Required]
    [MaxLength(255)]
    public string State { get; set; }

    public Guid? SessionId { get; set; }
    public Session? Session { get; set; }
}
