using WorkSphereAPI.Models;

namespace WorkSphereAPI.Repositories.Interfaces
{
    public interface ITaskRepository
    {
        Task<IEnumerable<TaskItem>> GetAllAsync();
    
        Task<int> CountAsync();
        Task<int> CountByStatusAsync(string status);
     
        Task<IEnumerable<TaskItem>> GetByAssignedUserIdAsync(int userId);
        Task<int> CountByAssignedUserIdAsync(int userId);
        Task<int> CountByAssignedUserAndStatusAsync(int userId, string status);
        Task<TaskItem?> GetByIdAsync(int id);
        Task AddAsync(TaskItem task);
        void Update(TaskItem task);
        Task<bool> SaveChangesAsync();
      


    }
}

