namespace Test_App.Models.DTO
{
    public class ContactMessageDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }   // or string if you changed it
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public string Status { get; set; } = "Pending";
    }

}
