using System;
using System.Collections.Generic;

namespace Test_App.Models.Entities;

public partial class User
{
    public int UserId { get; set; }

    public string Username { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public int PersonId { get; set; }

    public virtual Person Person { get; set; } = null!;
    public virtual ICollection<Todo> Todos { get; set; } = new List<Todo>();
    public virtual ICollection<TodoHistory> TodoHistories { get; set; } = new List<TodoHistory>();
    public virtual ICollection<ContactMessage> ContactMessages { get; set; } = new List<ContactMessage>();
    public virtual ICollection<Post> Posts { get; set; } = new List<Post>();
    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();


}
