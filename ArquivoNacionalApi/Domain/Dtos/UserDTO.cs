namespace ArquivoNacionalApi.Domain.Dtos
{
    public class UserDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string State { get; set; }
        public Guid? SessionId { get; set; }
    }
}
