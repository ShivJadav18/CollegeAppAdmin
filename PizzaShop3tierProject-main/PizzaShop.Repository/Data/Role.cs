using System;
using System.Collections.Generic;

namespace PizzaShop.Repository.Data;

public partial class Role
{
    public int RoleId { get; set; }

    public string Name { get; set; } = null!;

    public bool? Isdeleted { get; set; }

    public DateTime? Createdat { get; set; }

    public DateTime? Updatedat { get; set; }

    public virtual ICollection<Permission> Permissions { get; } = new List<Permission>();

    public virtual ICollection<User> Users { get; } = new List<User>();
}
