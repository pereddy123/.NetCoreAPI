using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WorkSphereAPI.DTOs;
using WorkSphereAPI.Repositories.Interfaces;
using WorkSphereAPI.Services.Interfaces;

namespace WorkSphereAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _taskService;
        private readonly ITaskCommentService _taskCommentService;
        private readonly IUserRepository _userRepository;

        public TasksController(
            ITaskService taskService,
            ITaskCommentService taskCommentService,
            IUserRepository userRepository)
        {
            _taskService = taskService;
            _taskCommentService = taskCommentService;
            _userRepository = userRepository;
        }

    
        // Admin/Manager: Get all tasks
        [HttpGet]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<ActionResult<IEnumerable<TaskDto>>> GetAllTasks()
        {
            var tasks = await _taskService.GetAllTasksAsync();
            return Ok(tasks);
        }

        // Employee: Get tasks assigned to current user
        [HttpGet("assigned")]
        [Authorize(Roles = "Employee")]
        public async Task<ActionResult<IEnumerable<TaskDto>>> GetAssignedTasks()
        {
            var userIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!int.TryParse(userIdStr, out int userId))
                return Unauthorized();

            var tasks = await _taskService.GetTasksByUserIdAsync(userId);
            return Ok(tasks);
        }

        // Admin/Manager: Create task
        [HttpPost]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> CreateTask([FromBody] CreateTaskRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var success = await _taskService.CreateTaskAsync(request);
            if (!success)
                return BadRequest("Could not create task.");

            return Ok(new { message = "Task created successfully" });

        }
     
        [HttpPut("{id}/status")]
        [Authorize(Roles = "Employee")]
        public async Task<IActionResult> UpdateTaskStatus(int id, [FromBody] string newStatus)
        {
            var userIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!int.TryParse(userIdStr, out int userId))
                return Unauthorized();

            var result = await _taskService.UpdateTaskStatusAsync(id, userId, newStatus);
            if (!result) return Forbid();

            return Ok(new { message = "Task status updated" });
        }

        [HttpPost("{taskId}/comments")]
        [Authorize(Roles = "Employee")]
        public async Task<IActionResult> AddComment(int taskId, [FromBody] string commentText)
        {
            var username = User.FindFirst(ClaimTypes.Name)?.Value;
            var user = (await _userRepository.GetAllAsync()).FirstOrDefault(u => u.Username == username);

            if (user == null) return Unauthorized();

            var result = await _taskCommentService.AddCommentAsync(taskId, user.Id, commentText);
            if (!result)
                return Forbid();

            return Ok("Comment added successfully.");
        }

        [HttpGet("{taskId}/comments")]
        [Authorize]
        public async Task<IActionResult> GetComments(int taskId)
        {
            var comments = await _taskCommentService.GetCommentsByTaskIdAsync(taskId);
            var result = comments.Select(c => new
            {
                c.CommentText,
                c.Timestamp,
                CommentedBy = c.CommentedBy?.Username
            });

            return Ok(result);
        }



    }
}

