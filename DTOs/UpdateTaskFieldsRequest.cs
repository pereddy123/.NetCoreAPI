namespace WorkSphereAPI.DTOs
{
    public class UpdateTaskFieldsRequest
    {
        public string? Status { get; set; } = "New";
        public int Progress { get; set; } = 0;
        public string? Comment { get; set; }
    }

}
