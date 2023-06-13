using System;
using System.Collections.Generic;

namespace UserManage.entities.Models;

public partial class State
{
    public long StateId { get; set; }

    public string? StateName { get; set; }

    public virtual ICollection<City> Cities { get; set; } = new List<City>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
