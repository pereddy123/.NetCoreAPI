using AutoMapper;
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
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _mapper = mapper;
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

        public async Task<User> CreateUserAsync(CreateUserRequest request)
        {
            var existingUser = (await _userRepository.GetAllAsync())
                .Any(u => u.Username.ToLower() == request.Username.ToLower());

            if (existingUser) return null;

            var user = new User
            {
                Username = request.Username,
                PasswordHash = Convert.ToBase64String(Encoding.UTF8.GetBytes(request.Password)),
                Role = request.Role
            };

            await _userRepository.AddAsync(user);
            await _userRepository.SaveChangesAsync();

            return user;
        }

        public async Task<UserDto> UpdateUserAsync(int id, UpdateUserRequest request)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null) return null;

            if (!string.IsNullOrWhiteSpace(request.Password))
            {
                user.PasswordHash = Convert.ToBase64String(Encoding.UTF8.GetBytes(request.Password));
            }

            user.Role = request.Role;
            await _userRepository.SaveChangesAsync();

            return _mapper.Map<UserDto>(user);
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
