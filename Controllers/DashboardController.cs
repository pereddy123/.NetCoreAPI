using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkSphereAPI.Services.Interfaces;

namespace WorkSphereAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService _dashboardService;

        public DashboardController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

    
        [HttpGet("admin-summary")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAdminSummary()
        {
            var summary = await _dashboardService.GetAdminSummaryAsync();
            return Ok(summary);
        }

        [HttpGet("manager-summary")]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> GetManagerSummary()
        {
            var summary = await _dashboardService.GetManagerSummaryAsync();
            return Ok(summary);
        }

        [HttpGet("employee-summary")]
        [Authorize(Roles = "Employee")]
        public async Task<IActionResult> GetEmployeeSummary()
        {
            var username = User.Identity?.Name;
            if (string.IsNullOrEmpty(username)) return Unauthorized();

            var summary = await _dashboardService.GetEmployeeSummaryAsync(username);
            return Ok(summary);
        }

    }
}
