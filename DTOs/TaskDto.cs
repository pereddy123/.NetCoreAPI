namespace WorkSphereAPI.DTOs
{
    public class TaskDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Status { get; set; } = "New";
        public DateTime? DueDate { get; set; }
        public string AssignedToUsername { get; set; } = string.Empty;
    }
}

