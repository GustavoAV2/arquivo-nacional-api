using ArquivoNacionalApi.Data.Repositories;
using ArquivoNacionalApi.Domain.Dtos;
using ArquivoNacionalApi.Domain.Entities;
using System.Linq;

namespace ArquivoNacionalApi.Services
{
    public class SessionService : ISessionService
    {
        private readonly ISessionRepository _sessionRepository;
        private readonly IUserRepository _userRepository;
        private readonly IDocumentMetadataRepository _documentMetadataRepository;

        public SessionService(ISessionRepository sessionRepository, IUserRepository userRepository, IDocumentMetadataRepository documentMetadataRepository)
        {
            _sessionRepository = sessionRepository;
            _userRepository = userRepository;
            _documentMetadataRepository = documentMetadataRepository;
        }

        public ActiveSessionDTO GetSessionActiveInfoByUserId(Guid userId)
        {
            var session = _sessionRepository.GetSessionByUserId(userId).FirstOrDefault();
            var players = _userRepository.GetUsersBySessionId(session.Id);

            var sessionDtos = new ActiveSessionDTO()
            {
                Id = session.Id,
                DocumentId = session.DocumentId,
                PlayerLimit = session.PlayerLimit,
                Players = players.Select(u => new PlayerDto()
                {
                    Name = u.Name,
                    Points = CalcSessionPoints(u.Id, session.Id)
                }).ToList()
            };
            return sessionDtos;
        }

        private int CalcSessionPoints(Guid userId, Guid documentId)
        {
            var session = _documentMetadataRepository.GetDocumentMetadataByUserIdAndDocumentIdAsync(userId, documentId);
            return session.Points;
        }

        public async Task CreateSession(CreateSessionDTO sessionDto)
        {
            var user = await _userRepository.GetByIdAsync(sessionDto.UserId);
            var session = new Session
            {
                DocumentId = sessionDto.DocumentId,
                PlayerLimit = sessionDto.PlayerLimit,
                Users = new List<User>() { user }
            };
            _sessionRepository.Add(session);
        }

        public IEnumerable<SessionDTO> GetSessionsListByUserIdAsync(Guid userId)
        {
            var docsByUser = _documentMetadataRepository.GetDocumentsByUserId(userId);
            var sessions = _sessionRepository.GetSessionPorDocumentosId(docsByUser.Select(d => d.Id).ToList());
            var sessionDtos = sessions.Select(s => new SessionDTO { Id = s.Id, DocumentId = s.DocumentId, PlayerLimit = s.PlayerLimit });
            return sessionDtos;
        }

        public async Task<IEnumerable<SessionDTO>> GetAllSessionsAsync()
        {
            var sessions = await _sessionRepository.GetAllAsync();
            var sessionDtos = sessions.Select(s => new SessionDTO { Id = s.Id, DocumentId = s.DocumentId, PlayerLimit = s.PlayerLimit });
            return sessionDtos;
        }

        public async Task<SessionDTO> GetSessionByIdAsync(Guid id)
        {
            var session = await _sessionRepository.GetByIdAsync(id);
            return new SessionDTO { Id = session.Id, DocumentId = session.DocumentId, PlayerLimit = session.PlayerLimit };
        }

        public async Task<bool> UpdateSessionAsync(Guid id, UpdateSessionDTO session)
        {

            var listUsers = new List<User>();
            foreach (var idFound in session.UserIds)
            {
                listUsers.Add(await _userRepository.GetByIdAsync(idFound));
            }

            var foundSession = await _sessionRepository.GetByIdAsync(id);

            if (foundSession != null)
            {
                foundSession.PlayerLimit = session.PlayerLimit;
                foundSession.Users.AddRange(listUsers.Where(u => !foundSession.Users.Exists(f => f.Id == u.Id)));
                _sessionRepository.Update(foundSession);
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteSessionAsync(Guid id)
        {
            var foundSession = await _sessionRepository.GetByIdAsync(id);

            if (foundSession != null)
            {
                _sessionRepository.Delete(foundSession);
                return true;
            }
            return false;
        }
    }

    public interface ISessionService
    {
        ActiveSessionDTO GetSessionActiveInfoByUserId(Guid userId);
        Task CreateSession(CreateSessionDTO sessionDto);
        IEnumerable<SessionDTO> GetSessionsListByUserIdAsync(Guid userId);
        Task<IEnumerable<SessionDTO>> GetAllSessionsAsync();
        Task<SessionDTO> GetSessionByIdAsync(Guid id);
        Task<bool> UpdateSessionAsync(Guid id, UpdateSessionDTO session);
        Task<bool> DeleteSessionAsync(Guid id);
    }
}
