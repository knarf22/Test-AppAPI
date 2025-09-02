using System;
using System.Collections.Generic;

namespace Test_App.Models.Entities;

public partial class Person
{
    public string? LastName { get; set; }

    public string? FirstName { get; set; }

    public string? MiddleName { get; set; }

    public string? City { get; set; }

    public int? Age { get; set; }

    public int PersonId { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
