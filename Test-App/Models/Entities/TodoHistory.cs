namespace Test_App.Models.Entities
{
    public class TodoHistory
    {
        public int HistoryId { get; set; }
        public int TodoId { get; set; }
        public int UserId { get; set; }   // 👈 link to User
        public string Action { get; set; } = null!; // e.g., Created, Updated, Completed, Deleted
        public DateTime ActionDate { get; set; }

        // Optional old/new values for tracking changes
        public string? OldTitle { get; set; }
        public string? NewTitle { get; set; }
        public string? OldDescription { get; set; }
        public string? NewDescription { get; set; }

        // Relationships
        public virtual Todo Todo { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
