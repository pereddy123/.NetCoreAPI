using WorkSphereAPI.DTOs;

namespace WorkSphereAPI.Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAllUsersAsync();
        Task<bool> CreateUserAsync(CreateUserRequest request);
        Task<bool> UpdateUserAsync(int id, UpdateUserRequest request);
        Task<bool> DeleteUserAsync(int id);

    }
}
