using Microsoft.EntityFrameworkCore;
using WorkSphereAPI.DTOs;
using WorkSphereAPI.Models;
using WorkSphereAPI.Repositories.Interfaces;
using WorkSphereAPI.Services.Interfaces;

namespace WorkSphereAPI.Services.Implementations
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;

        public TaskService(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<IEnumerable<TaskDto>> GetAllTasksAsync()
        {
            var tasks = await _taskRepository.GetAllAsync();
            return tasks.Select(t => new TaskDto
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                Status = t.Status,
                AssignedToUsername = t.AssignedToUser?.Username ?? "Unassigned",
                DueDate = t.DueDate
            });
        }

        public async Task<IEnumerable<TaskDto>> GetTasksByUserIdAsync(int userId)
        {
            var tasks = await _taskRepository.GetByAssignedUserIdAsync(userId);
            return tasks.Select(t => new TaskDto
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                Status = t.Status,
                AssignedToUsername = t.AssignedToUser?.Username ?? "Unassigned",
                DueDate = t.DueDate
            });
        }

        public async Task<TaskDto?> GetTaskByIdAsync(int id)
        {
            var task = await _taskRepository.GetByIdAsync(id);
            if (task == null) return null;

            return new TaskDto
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                Status = task.Status,
                AssignedToUsername = task.AssignedToUser?.Username ?? "Unassigned",
                DueDate = task.DueDate
            };
        }

        public async Task<bool> CreateTaskAsync(CreateTaskRequest request)
        {
            var task = new TaskItem
            {
                Title = request.Title,
                Description = request.Description,
                Status = "New",
                AssignedToUserId = request.AssignedToUserId,
                DueDate = request.DueDate
            };

            await _taskRepository.AddAsync(task);
            return await _taskRepository.SaveChangesAsync();
        }

  
        public async Task<bool> UpdateTaskStatusAsync(int taskId, int userId, string newStatus)
        {
            var task = await _taskRepository.GetByIdAsync(taskId);
            if (task == null || task.AssignedToUserId != userId)
                return false;

            task.Status = newStatus;
            _taskRepository.Update(task);
            return await _taskRepository.SaveChangesAsync();
        }
        public async Task<bool> UpdateTaskFieldsAsync(int taskId, int userId, UpdateTaskFieldsRequest request)
        {
            var task = await _taskRepository.GetByIdAsync(taskId);
            if (task == null || task.AssignedToUserId != userId) return false;

            task.Status = request.Status;
            task.Progress = request.Progress;
            task.Comment = request.Comment;

            _taskRepository.Update(task);

            return await _taskRepository.SaveChangesAsync();
        }


    }
}
