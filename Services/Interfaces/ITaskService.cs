using WorkSphereAPI.DTOs;

namespace WorkSphereAPI.Services.Interfaces
{
    public interface ITaskService
    {
        Task<IEnumerable<TaskDto>> GetAllTasksAsync();
        Task<IEnumerable<TaskDto>> GetTasksByUserIdAsync(int userId);
        Task<TaskDto?> GetTaskByIdAsync(int id);
        Task<bool> CreateTaskAsync(CreateTaskRequest request);
     
        Task<bool> UpdateTaskStatusAsync(int taskId, int userId, string newStatus);


    }
}
