using System;
using System.Collections.Generic;

namespace PizzaShop.Repository.Data;

public partial class Itemtomodifiergroup
{
    public int ItemtogroupId { get; set; }

    public int? ItemId { get; set; }

    public int? ModifiergroupId { get; set; }

    public bool? Isdeleted { get; set; }

    public int? Minval { get; set; }

    public int? Maxval { get; set; }

    public virtual Item? Item { get; set; }

    public virtual Modifiergroup? Modifiergroup { get; set; }
}
