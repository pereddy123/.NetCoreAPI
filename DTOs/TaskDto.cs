namespace WorkSphereAPI.DTOs
{
    public class TaskDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public DateTime? DueDate { get; set; }
        public string AssignedToUsername { get; set; }

        public int Progress { get; set; }
        public string Comment { get; set; }
    }

}

