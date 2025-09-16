namespace Test_App.Models.Entities
{
    public class Todo
    {
        public int TodoId { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public bool IsDone { get; set; }  // changed from IsCompleted -> IsDone to match schema
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; } // 👈 for soft delete

        // Relationships
        public virtual User User { get; set; } = null!;
        public virtual ICollection<TodoHistory> Histories { get; set; } = new List<TodoHistory>();
    }
}
