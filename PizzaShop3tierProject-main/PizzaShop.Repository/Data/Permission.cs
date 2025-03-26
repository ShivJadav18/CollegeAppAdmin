using System;
using System.Collections.Generic;

namespace PizzaShop.Repository.Data;

public partial class Permission
{
    public int PermissionId { get; set; }

    public int PermissionfieldId { get; set; }

    public int RoleId { get; set; }

    public bool Canview { get; set; }

    public bool Canadd { get; set; }

    public bool Candelete { get; set; }

    public int? Createdby { get; set; }

    public DateTime? Createdat { get; set; }

    public int? Updatedby { get; set; }

    public DateTime? Updatedat { get; set; }

    public bool? Isdeleted { get; set; }

    public virtual User? CreatedbyNavigation { get; set; }

    public virtual Permissionfield Permissionfield { get; set; } = null!;

    public virtual Role Role { get; set; } = null!;

    public virtual User? UpdatedbyNavigation { get; set; }
}
