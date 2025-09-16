namespace Test_App.Models.DTO
{
    // For creating/updating a Todo
    public class TodoRequest
    {
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public int UserId { get; set; }   // link to User
    }

    // For returning a Todo from the API
    public class TodoResponse
    {
        public int TodoId { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public bool IsDone { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
