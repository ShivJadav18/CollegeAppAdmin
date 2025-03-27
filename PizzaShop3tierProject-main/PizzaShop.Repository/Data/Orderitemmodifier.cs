using System;
using System.Collections.Generic;

namespace PizzaShop.Repository.Data;

public partial class Orderitemmodifier
{
    public int OrderitemmodifierId { get; set; }

    public int? OrdertoitemId { get; set; }

    public int? ModifierId { get; set; }

    public int? Quantity { get; set; }

    public virtual Modifier? Modifier { get; set; }

    public virtual Ordertoitem? Ordertoitem { get; set; }
}
