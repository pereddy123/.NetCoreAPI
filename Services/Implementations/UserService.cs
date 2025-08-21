using System.Text;
using WorkSphereAPI.DTOs;
using WorkSphereAPI.Models;
using WorkSphereAPI.Repositories.Interfaces;
using WorkSphereAPI.Services.Interfaces;

namespace WorkSphereAPI.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllAsync();

            // Map entities to DTOs manually
            return users.Select(u => new UserDto
            {
                Id = u.Id,
                Username = u.Username,
                Role = u.Role
            });
        }
        public async Task<bool> CreateUserAsync(CreateUserRequest request)
        {
            var userExists = (await _userRepository.GetAllAsync())
                .Any(u => u.Username.ToLower() == request.Username.ToLower());

            if (userExists) return false;

            var user = new User
            {
                Username = request.Username,
                PasswordHash = Convert.ToBase64String(Encoding.UTF8.GetBytes(request.Password)), // Simple PoC hash
                Role = request.Role
            };

            await _userRepository.AddAsync(user);
            return await _userRepository.SaveChangesAsync();
        }
        public async Task<bool> UpdateUserAsync(int id, UpdateUserRequest request)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null) return false;

            user.Username = request.Username;
            user.Role = request.Role;

            _userRepository.Update(user);
            return await _userRepository.SaveChangesAsync();
        }
        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null) return false;

            _userRepository.Delete(user);
            return await _userRepository.SaveChangesAsync();
        }

    }
}
