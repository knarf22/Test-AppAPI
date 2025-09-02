namespace Test_App.Models.DTO
{
    public class UserLoginRequest
    {
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
    public class UserLoginResponse
    {
        public int UserId { get; set; }
        public string Username { get; set; } = null!;
        public string Token { get; set; } = null!;
    }

}
