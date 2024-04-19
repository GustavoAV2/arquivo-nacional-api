using ArquivoNacionalApi.Domain.Entities;

namespace ArquivoNacionalApi.Data.Repositories
{

    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(DatabaseContext context) : base(context)
        { }

        public List<User> GetUsersBySessionId(Guid sessionId)
        {
            return _context.Users
                .Where(u => u.SessionId == sessionId)
                .ToList();
        }
    }

    public interface IUserRepository : IRepository<User> 
    {
        List<User> GetUsersBySessionId(Guid sessionId);
    }
}
