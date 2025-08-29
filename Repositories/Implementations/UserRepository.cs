using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WorkSphereAPI.Data;
using WorkSphereAPI.Models;
using WorkSphereAPI.Repositories.Interfaces;

namespace WorkSphereAPI.Repositories.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<int> CountAsync()
        {
            return await _context.Users.CountAsync();
        }
        public async Task<User?> GetByIdAsync(int id)
        {

            return await _context.Users.FindAsync(id);
        }
        public async Task<User?> GetByUsernameAsync(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(u =>
                u.Username.Trim().ToLower() == username.Trim().ToLower()
            );
        }

        public async Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
        }
        public async Task<User> CreateAsync(User user, string password)
        {
            var hasher = new PasswordHasher<User>();
            user.PasswordHash = hasher.HashPassword(user, password);

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public void Update(User user)
        {
            _context.Users.Update(user);
        }

        public void Delete(User user)
        {
            _context.Users.Remove(user);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
