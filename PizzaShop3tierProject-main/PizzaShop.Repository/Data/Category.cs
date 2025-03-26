using System;
using System.Collections.Generic;

namespace PizzaShop.Repository.Data;

public partial class Category
{
    public int CategoryId { get; set; }

    public string Categoryname { get; set; } = null!;

    public string? Description { get; set; }

    public int? Createdby { get; set; }

    public DateTime? Createdat { get; set; }

    public int? Updatedby { get; set; }

    public DateTime? Updatedat { get; set; }

    public bool? Isdeleted { get; set; }

    public virtual User? CreatedbyNavigation { get; set; }

    public virtual ICollection<Item> Items { get; } = new List<Item>();

    public virtual User? UpdatedbyNavigation { get; set; }
}
