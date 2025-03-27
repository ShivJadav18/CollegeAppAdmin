using System;
using System.Collections.Generic;

namespace PizzaShop.Repository.Data;

public partial class Tax
{
    public int TaxId { get; set; }

    public string? Name { get; set; }

    public bool Isenabled { get; set; }

    public bool? Isdefault { get; set; }

    public string? Taxtype { get; set; }

    public decimal? Amount { get; set; }

    public DateTime? Createdat { get; set; }

    public DateTime? Updatedat { get; set; }

    public int? Createdby { get; set; }

    public int? Updatedby { get; set; }

    public bool? Isdeleted { get; set; }

    public virtual User? CreatedbyNavigation { get; set; }

    public virtual ICollection<Ordertotax> Ordertotaxes { get; } = new List<Ordertotax>();

    public virtual User? UpdatedbyNavigation { get; set; }
}
