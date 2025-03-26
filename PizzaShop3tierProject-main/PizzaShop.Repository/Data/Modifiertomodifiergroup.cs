using System;
using System.Collections.Generic;

namespace PizzaShop.Repository.Data;

public partial class Modifiertomodifiergroup
{
    public int ModifiertogroupId { get; set; }

    public int ModifiergroupId { get; set; }

    public int ModifierId { get; set; }

    public bool? Isdeleted { get; set; }

    public virtual Modifier Modifier { get; set; } = null!;

    public virtual Modifiergroup Modifiergroup { get; set; } = null!;
}
