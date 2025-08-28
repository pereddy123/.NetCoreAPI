namespace WorkSphereAPI.Models
{
    public class TaskItem
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Status { get; set; } = "New";
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? DueDate { get; set; }

        public int AssignedToUserId { get; set; }
        public User? AssignedToUser { get; set; }

        // ✅ New fields
        public int Progress { get; set; } = 0; // 0–100%
        public string Comment { get; set; } = string.Empty;
    }


}
