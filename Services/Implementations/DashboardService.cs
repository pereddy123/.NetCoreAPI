using WorkSphereAPI.Repositories.Interfaces;
using WorkSphereAPI.Services.Interfaces;

namespace WorkSphereAPI.Services.Implementations
{
    public class DashboardService : IDashboardService
    {
        private readonly IUserRepository _userRepo;
        private readonly ITaskRepository _taskRepo;

        public DashboardService(IUserRepository userRepo, ITaskRepository taskRepo)
        {
            _userRepo = userRepo;
            _taskRepo = taskRepo;
        }

        public async Task<object> GetAdminSummaryAsync()
        {
            var totalUsers = await _userRepo.CountAsync();
            var totalTasks = await _taskRepo.CountAsync();

            var newCount = await _taskRepo.CountByStatusAsync("New");
            var inProgress = await _taskRepo.CountByStatusAsync("In Progress");
            var completed = await _taskRepo.CountByStatusAsync("Completed");

            return new
            {
                totalUsers,
                totalTasks,
                @new = newCount,
                inProgress,
                completed
            };
        }

        public async Task<object> GetManagerSummaryAsync()
        {
            var totalTasks = await _taskRepo.CountAsync();
            var newCount = await _taskRepo.CountByStatusAsync("New");
            var inProgress = await _taskRepo.CountByStatusAsync("In Progress");
            var completed = await _taskRepo.CountByStatusAsync("Completed");

            return new
            {
                totalTasks,
                @new = newCount,
                inProgress = inProgress,
                completed
            };
        }


        public async Task<object> GetEmployeeSummaryAsync(string username)
        {
            var user = await _userRepo.GetByUsernameAsync(username);
            if (user == null) return new { totalTasks = 0,  @new = 0, inProgress = 0, completed = 0 };

            var totalTasks = await _taskRepo.CountByAssignedUserIdAsync(user.Id);
            var newCount = await _taskRepo.CountByAssignedUserAndStatusAsync(user.Id, "New");
            var inProgress = await _taskRepo.CountByAssignedUserAndStatusAsync(user.Id, "In Progress");
            var completed = await _taskRepo.CountByAssignedUserAndStatusAsync(user.Id, "Completed");

            return new
            {
                totalTasks,
                @new = newCount,
                inProgress,
                completed
            };
        }

    }
}
