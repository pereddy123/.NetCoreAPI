using WorkSphereAPI.Models;
using WorkSphereAPI.Repositories.Interfaces;
using WorkSphereAPI.Services.Interfaces;

namespace WorkSphereAPI.Services.Implementations
{
    public class TaskCommentService : ITaskCommentService
    {
        private readonly ITaskCommentRepository _commentRepo;
        private readonly ITaskRepository _taskRepo;

        public TaskCommentService(
            ITaskCommentRepository commentRepo,
            ITaskRepository taskRepo)
        {
            _commentRepo = commentRepo;
            _taskRepo = taskRepo;
        }

        public async Task<IEnumerable<TaskComment>> GetCommentsByTaskIdAsync(int taskId)
        {
            return await _commentRepo.GetByTaskIdAsync(taskId);
        }

        public async Task<bool> AddCommentAsync(int taskId, int userId, string commentText)
        {
            var task = await _taskRepo.GetByIdAsync(taskId);
            if (task == null || task.AssignedToUserId != userId)
                return false;

            var comment = new TaskComment
            {
                TaskItemId = taskId,
                CommentedByUserId = userId,
                CommentText = commentText
            };

            await _commentRepo.AddAsync(comment);
            return await _commentRepo.SaveChangesAsync();
        }
    }
}
