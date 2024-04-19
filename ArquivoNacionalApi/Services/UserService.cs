using ArquivoNacionalApi.Data.Repositories;
using ArquivoNacionalApi.Domain.Dtos;
using ArquivoNacionalApi.Domain.Entities;

namespace ArquivoNacionalApi.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepository;

        public UserService(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<UserDTO>> GetAllUsersAsync()
        {
            var allUsers = await _userRepository.GetAllAsync();
            return allUsers.Select(u => new UserDTO()
            {
                Email = u.Email,
                Name = u.Name,
                Password = u.Password,
                State = u.State,
                SessionId = u.SessionId.Value
            });
        }

        public async Task<UserDTO> GetUserByIdAsync(Guid id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            return new UserDTO()
            {
                Email = user.Email,
                Name = user.Name,
                Password = user.Password,
                State = user.State,
                SessionId = user.SessionId.Value
            };
        }

        public void CreateUser(UserDTO userdto)
        {
            var user = new User()
            {
                Email = userdto.Email,
                Name = userdto.Name,
                Password = userdto.Password,
                State = userdto.State,
                SessionId = userdto.SessionId.Value
            };
            _userRepository.Add(user);
        }

        public async Task<bool> UpdateUserAsync(Guid id, UserDTO userdto)
        {
            var foundUser = await _userRepository.GetByIdAsync(id);
            if (foundUser != null)
            {
                foundUser.Email = userdto.Email;
                foundUser.Name = userdto.Name;
                foundUser.Password = userdto.Password;
                foundUser.State = userdto.State;
                foundUser.SessionId = userdto.SessionId.Value;

                _userRepository.Update(foundUser);
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteUserAsync(Guid id)
        {
            var foundUser = await _userRepository.GetByIdAsync(id);
            if (foundUser != null)
            {
                _userRepository.Delete(foundUser);
                return true;
            }
            return false;
        }
    }

    public interface IUserService
    {
        Task<IEnumerable<UserDTO>> GetAllUsersAsync();
        Task<UserDTO> GetUserByIdAsync(Guid id);
        void CreateUser(UserDTO userdto);
        Task<bool> UpdateUserAsync(Guid id, UserDTO userdto);
        Task<bool> DeleteUserAsync(Guid id);
    }
}
