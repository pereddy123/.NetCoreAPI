using WorkSphereAPI.Models;

namespace WorkSphereAPI.Services.Interfaces
{
    public interface ITaskCommentService
    {
        Task<IEnumerable<TaskComment>> GetCommentsByTaskIdAsync(int taskId);
        Task<bool> AddCommentAsync(int taskId, int userId, string commentText);
    }
}
