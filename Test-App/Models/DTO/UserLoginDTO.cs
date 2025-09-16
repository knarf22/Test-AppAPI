namespace Test_App.Models.DTO
{
    public class UserLoginDTO
    {
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public int PersonId { get; set; }  // 👈 required since User links to Person

    }
    public class UserLoginResponse
    {
        public int UserId { get; set; }
        public string Username { get; set; } = null!;
        public string Token { get; set; } = null!;
        public int PersonId { get; set; }

    }

}
