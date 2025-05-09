using System;
using System.Collections.Generic;

namespace PizzaShop.Repository.Data;

public partial class Typetable
{
    public int Typeid { get; set; }

    public string? Typename { get; set; }

    public virtual ICollection<Item> Items { get; } = new List<Item>();
}
