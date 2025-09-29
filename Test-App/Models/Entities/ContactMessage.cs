namespace Test_App.Models.Entities
{
    public class ContactMessage
    {
        public int Id { get; set; }

        public  int UserId { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string Message { get; set; }

        public string Status { get; set; } = "Pending";
        public virtual User User { get; set; } = null!;

    }

}
