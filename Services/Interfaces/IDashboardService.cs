namespace WorkSphereAPI.Services.Interfaces
{
    public interface IDashboardService
    {
        Task<object> GetAdminSummaryAsync();
        Task<object> GetManagerSummaryAsync();
        Task<object> GetEmployeeSummaryAsync(string username);

    }
}

