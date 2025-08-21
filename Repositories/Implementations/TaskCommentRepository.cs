using Microsoft.EntityFrameworkCore;
using WorkSphereAPI.Data;
using WorkSphereAPI.Models;
using WorkSphereAPI.Repositories.Interfaces;

namespace WorkSphereAPI.Repositories.Implementations
{
    public class TaskCommentRepository : ITaskCommentRepository
    {
        private readonly ApplicationDbContext _context;

        public TaskCommentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TaskComment>> GetByTaskIdAsync(int taskId)
        {
            return await _context.TaskComments
                .Include(c => c.CommentedBy)
                .Where(c => c.TaskItemId == taskId)
                .OrderByDescending(c => c.Timestamp)
                .ToListAsync();
        }

        public async Task AddAsync(TaskComment comment)
        {
            await _context.TaskComments.AddAsync(comment);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
