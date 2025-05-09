using System;
using System.Collections.Generic;

namespace PizzaShop.Repository.Data;

public partial class Carttable
{
    public int Cartid { get; set; }

    public int Customerid { get; set; }

    public virtual ICollection<Cartdetailtable> Cartdetailtables { get; } = new List<Cartdetailtable>();

    public virtual Customer Customer { get; set; } = null!;
}
