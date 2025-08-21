using Microsoft.EntityFrameworkCore;
using WorkSphereAPI.Data;
using WorkSphereAPI.Models;
using WorkSphereAPI.Repositories.Interfaces;

namespace WorkSphereAPI.Repositories.Implementations
{
    public class TaskRepository : ITaskRepository
    {
        private readonly ApplicationDbContext _context;
        public TaskRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TaskItem>> GetAllAsync()
        {
            return await _context.Tasks.Include(t => t.AssignedToUser).ToListAsync();
        }

        public async Task<int> CountAsync()
        {
            return await _context.Tasks.CountAsync();
        }
        public async Task<int> CountByStatusAsync(string status)
        {
            return await _context.Tasks.CountAsync(t => t.Status == status);
        }

        public async Task<TaskItem?> GetByIdAsync(int id)
        {
            return await _context.Tasks.Include(t => t.AssignedToUser)
                                       .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<IEnumerable<TaskItem>> GetByAssignedUserIdAsync(int userId)
        {
            return await _context.Tasks.Where(t => t.AssignedToUserId == userId)
                                       .ToListAsync();
        }
        public async Task<int> CountByAssignedUserIdAsync(int userId)
        {
            return await _context.Tasks.CountAsync(t => t.AssignedToUserId == userId);
        }

        public async Task<int> CountByAssignedUserAndStatusAsync(int userId, string status)
        {
            return await _context.Tasks.CountAsync(t => t.AssignedToUserId == userId && t.Status == status);
        }


        public async Task AddAsync(TaskItem task)
        {
            await _context.Tasks.AddAsync(task);
        }

        public void Update(TaskItem task)
        {
            _context.Tasks.Update(task);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
