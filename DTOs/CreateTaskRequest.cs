﻿namespace WorkSphereAPI.DTOs
{
    public class CreateTaskRequest
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int AssignedToUserId { get; set; }  
        public DateTime? DueDate { get; set; }
    }

}
