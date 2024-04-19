using ArquivoNacionalApi.Domain.Entities;

namespace ArquivoNacionalApi.Data.Repositories
{
    public class SessionRepository : Repository<Session>, ISessionRepository
    {
        public SessionRepository(DatabaseContext context) : base(context) { }

        public List<Session> GetSessionPorDocumentosId(List<Guid> docsId)
        {
            return _context.Sessions.Where(s => docsId.Contains(s.DocumentId)).ToList();
        }

        public List<Session> GetSessionByUserId(Guid userId)
        {
            return _context.Sessions
                .Where(s => s.Users.Any(u => u.Id == userId)).
                ToList();
        }
    }

    public interface ISessionRepository : IRepository<Session>
    {
        List<Session> GetSessionPorDocumentosId(List<Guid> docsId);

        List<Session> GetSessionByUserId(Guid userId);
    }
}
