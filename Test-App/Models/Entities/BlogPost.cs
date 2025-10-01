namespace Test_App.Models.Entities
{
    public class Post
    {
        public int Id { get; set; }
        public int UserId { get; set; }   // foreign key
        public string Title { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }

        // Navigation property
        public virtual User User { get; set; } = null!;

        // Navigation property for related comments
        public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }

    public class Comment
    {
        public int CommentId { get; set; }
        public int PostId { get; set; }
        public int UserId { get; set; }
        public string Text { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }

        public virtual Post Post { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }

}
