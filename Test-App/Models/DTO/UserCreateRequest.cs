namespace Test_App.Models.DTO
{
    public class UserRegisterRequest
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string ConfirmPassword { get; set; } = string.Empty;
    }

    public class UserRegisterResponse
    {
        public int UserId { get; set; }
        public string Username { get; set; } = string.Empty;
    }
}
