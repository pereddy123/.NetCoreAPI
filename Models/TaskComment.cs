namespace WorkSphereAPI.Models
{
    public class TaskComment
    {
        public int Id { get; set; }
        public string CommentText { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        public int TaskItemId { get; set; }
        public TaskItem? TaskItem { get; set; }

        public int CommentedByUserId { get; set; }
        public User? CommentedBy { get; set; }
    }
}
