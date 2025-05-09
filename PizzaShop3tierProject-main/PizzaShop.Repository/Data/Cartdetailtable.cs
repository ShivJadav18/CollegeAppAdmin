using System;
using System.Collections.Generic;

namespace PizzaShop.Repository.Data;

public partial class Cartdetailtable
{
    public int Cartdetailid { get; set; }

    public int Cartid { get; set; }

    public int Customerid { get; set; }

    public int Itemid { get; set; }

    public int? Itemqun { get; set; }

    public virtual Carttable Cart { get; set; } = null!;

    public virtual Customer Customer { get; set; } = null!;

    public virtual Item Item { get; set; } = null!;
}
