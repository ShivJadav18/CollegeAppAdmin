using System;
using System.Collections.Generic;

namespace PizzaShop.Repository.Data;

public partial class Item
{
    public int ItemId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public decimal? Rate { get; set; }

    public int? Quantity { get; set; }

    public bool Isavailable { get; set; }

    public int? CategoryId { get; set; }

    public bool? Isdeleted { get; set; }

    public int? Typeid { get; set; }

    public bool? Defaulttax { get; set; }

    public decimal? Taxpercentage { get; set; }

    public string? Shortcode { get; set; }

    public string? Imageurl { get; set; }

    public DateTime? Createdat { get; set; }

    public DateTime? Updatedat { get; set; }

    public int? Createdby { get; set; }

    public int? Updatedby { get; set; }

    public virtual ICollection<Cartdetailtable> Cartdetailtables { get; } = new List<Cartdetailtable>();

    public virtual Category? Category { get; set; }

    public virtual User? CreatedbyNavigation { get; set; }

    public virtual ICollection<Favouriteitem> Favouriteitems { get; } = new List<Favouriteitem>();

    public virtual ICollection<Ordertoitem> Ordertoitems { get; } = new List<Ordertoitem>();

    public virtual Typetable? Type { get; set; }

    public virtual User? UpdatedbyNavigation { get; set; }
}
