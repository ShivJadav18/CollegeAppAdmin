using System;
using System.Collections.Generic;

namespace PizzaShop.Repository.Data;

public partial class Ordertotable
{
    public int OrdertotableId { get; set; }

    public int? TableId { get; set; }

    public int? OrderId { get; set; }

    public virtual Order? Order { get; set; }

    public virtual Table? Table { get; set; }
}
