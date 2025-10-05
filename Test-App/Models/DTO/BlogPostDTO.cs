namespace Test_App.Models.DTO
{
  
        public class PostDTO
        {
            public int PostId { get; set; }
            public int UserId { get; set; }       // Author id
            public string Username { get; set; } = string.Empty; // 👈 Add this

            public string Title { get; set; } = string.Empty;
            public string Body { get; set; } = string.Empty;
            public DateTime CreatedAt { get; set; }
            public List<CommentDTO> Comments { get; set; } = new();  // 👈 Add this


        }

        public class CreatePostDTO
        {
            public int UserId { get; set; }
            public string Title { get; set; } = string.Empty;
            public string Body { get; set; } = string.Empty;
        }

        public class UpdatePostDto
        {
            public string Title { get; set; } = string.Empty;
            public string Body { get; set; } = string.Empty;
        }
        public class CommentDTO
        {
            public int CommentId { get; set; }
            public int PostId { get; set; }
            public int UserId { get; set; }
            public string Username { get; set; } = string.Empty; // 👈 add this
            public string Text { get; set; } = string.Empty;
            public DateTime CreatedAt { get; set; }
        }
        public class CreateCommentDTO
        {
            public int PostId { get; set; }
            public int UserId { get; set; }
            public string Text { get; set; } = string.Empty;
        }
        public class UpdateCommentDto
        {
            public string Text { get; set; } = string.Empty;
        }

}
