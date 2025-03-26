using System;
using System.Collections.Generic;

namespace PizzaShop.Repository.Data;

public partial class Unit
{
    public int UnitId { get; set; }

    public string? Name { get; set; }

    public string? Shortname { get; set; }

    public DateTime? Createdat { get; set; }

    public DateTime? Updatedat { get; set; }

    public int? Createdby { get; set; }

    public int? Updatedby { get; set; }

    public virtual User? CreatedbyNavigation { get; set; }

    public virtual ICollection<Item> Items { get; } = new List<Item>();

    public virtual ICollection<Modifier> Modifiers { get; } = new List<Modifier>();

    public virtual User? UpdatedbyNavigation { get; set; }
}
