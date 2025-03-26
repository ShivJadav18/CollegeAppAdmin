using System;
using System.Collections.Generic;

namespace PizzaShop.Repository.Data;

public partial class Permissionfield
{
    public int PermissionfieldId { get; set; }

    public string? Permissionfieldname { get; set; }

    public DateTime? Createdat { get; set; }

    public DateTime? Updatedat { get; set; }

    public int Createdby { get; set; }

    public int Updatedby { get; set; }

    public bool? Isdeleted { get; set; }

    public virtual User CreatedbyNavigation { get; set; } = null!;

    public virtual ICollection<Permission> Permissions { get; } = new List<Permission>();

    public virtual User UpdatedbyNavigation { get; set; } = null!;
}
