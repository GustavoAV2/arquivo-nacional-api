namespace ArquivoNacionalApi.Domain.Dtos
{
    public class SessionDTO
    {
        public Guid Id { get; set; }
        public Guid DocumentId { get; set; }
        public int PlayerLimit { get; set; }
    }

    public class CreateSessionDTO
    {
        public Guid Id { get; set; }
        public Guid DocumentId { get; set; }
        public Guid UserId { get; set; }
        public int PlayerLimit { get; set; }
    }

    public class UpdateSessionDTO
    {
        public Guid Id { get; set; }
        public Guid DocumentId { get; set; }
        public List<Guid> UserIds { get; set; }
        public int PlayerLimit { get; set; }
    }

    public class ActiveSessionDTO
    {
        public Guid Id { get; set; }
        public Guid DocumentId { get; set; }
        public int PlayerLimit { get; set; }
        public List<PlayerDto> Players { get; set; }
    }

    public class PlayerDto
    {
        public string Name { get; set; }
        public int Points { get; set; }
    }
}
