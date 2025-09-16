namespace Test_App.Models.DTO
{
    public class TodoHistoryResponse
    {
        public int HistoryId { get; set; }
        public int TodoId { get; set; }
        public int UserId { get; set; }
        public string Action { get; set; } = null!;
        public DateTime ActionDate { get; set; }

        public string? OldTitle { get; set; }
        public string? NewTitle { get; set; }
        public string? OldDescription { get; set; }
        public string? NewDescription { get; set; }
    }
}
