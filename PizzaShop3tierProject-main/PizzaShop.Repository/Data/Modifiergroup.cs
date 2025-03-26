using System;
using System.Collections.Generic;

namespace PizzaShop.Repository.Data;

public partial class Modifiergroup
{
    public int ModifiergroupId { get; set; }

    public string Groupname { get; set; } = null!;

    public string? Description { get; set; }

    public bool? Isdeleted { get; set; }

    public DateTime? Createdat { get; set; }

    public DateTime? Updatedat { get; set; }

    public int? Createdby { get; set; }

    public int? Updatedby { get; set; }

    public virtual User? CreatedbyNavigation { get; set; }

    public virtual ICollection<Itemtomodifiergroup> Itemtomodifiergroups { get; } = new List<Itemtomodifiergroup>();

    public virtual ICollection<Modifiertomodifiergroup> Modifiertomodifiergroups { get; } = new List<Modifiertomodifiergroup>();

    public virtual User? UpdatedbyNavigation { get; set; }
}
