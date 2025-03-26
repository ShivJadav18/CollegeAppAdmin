using System;
using System.Collections.Generic;

namespace PizzaShop.Repository.Data;

public partial class Ordertoitem
{
    public int OrdertoitemId { get; set; }

    public int? OrderId { get; set; }

    public int? ItemId { get; set; }

    public string? Instruction { get; set; }

    public int? Quantity { get; set; }

    public decimal? Amount { get; set; }

    public string? Status { get; set; }

    public virtual Item? Item { get; set; }

    public virtual Order? Order { get; set; }

    public virtual ICollection<Orderitemmodifier> Orderitemmodifiers { get; } = new List<Orderitemmodifier>();
}
