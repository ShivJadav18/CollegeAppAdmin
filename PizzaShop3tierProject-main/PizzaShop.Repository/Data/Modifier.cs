using System;
using System.Collections.Generic;

namespace PizzaShop.Repository.Data;

public partial class Modifier
{
    public int ModifierId { get; set; }

    public string Modifiername { get; set; } = null!;

    public decimal Rate { get; set; }

    public int UnitId { get; set; }

    public int Quantity { get; set; }

    public string? Description { get; set; }

    public bool? Isdeleted { get; set; }

    public DateTime? Createdat { get; set; }

    public DateTime? Updatedat { get; set; }

    public int Createdby { get; set; }

    public int Updatedby { get; set; }

    public virtual User CreatedbyNavigation { get; set; } = null!;

    public virtual ICollection<Modifiertomodifiergroup> Modifiertomodifiergroups { get; } = new List<Modifiertomodifiergroup>();

    public virtual ICollection<Orderitemmodifier> Orderitemmodifiers { get; } = new List<Orderitemmodifier>();

    public virtual Unit Unit { get; set; } = null!;

    public virtual User UpdatedbyNavigation { get; set; } = null!;
}
