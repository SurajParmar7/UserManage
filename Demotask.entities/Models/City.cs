using System;
using System.Collections.Generic;

namespace UserManage.entities.Models;

public partial class City
{
    public long CityId { get; set; }

    public string? CityName { get; set; }

    public long StateId { get; set; }

    public virtual State State { get; set; } = null!;

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
