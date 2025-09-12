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
}
