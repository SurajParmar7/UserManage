using System;
using System.Collections.Generic;

namespace UserManage.entities.Models;

public partial class User
{
    public long UserId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string StreetAddress { get; set; } = null!;

    public long? City { get; set; }

    public long? State { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public DateTime? DeletedAt { get; set; }

    public virtual City? CityNavigation { get; set; }

    public virtual State? StateNavigation { get; set; }
}
