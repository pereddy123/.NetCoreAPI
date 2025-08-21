using WorkSphereAPI.Models;

namespace WorkSphereAPI.Repositories.Interfaces
{
    public interface ITaskCommentRepository
    {
        Task<IEnumerable<TaskComment>> GetByTaskIdAsync(int taskId);
        Task AddAsync(TaskComment comment);
        Task<bool> SaveChangesAsync();
    }
}
