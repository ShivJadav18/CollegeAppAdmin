using System;
using System.Collections.Generic;

namespace PizzaShop.Repository.Data;

public partial class Order
{
    public int OrderId { get; set; }

    public int? CustomerId { get; set; }

    public string? Orderstatus { get; set; }

    public DateTime? Orderdate { get; set; }

    public decimal? Totalamount { get; set; }

    public int? Noofperson { get; set; }

    public DateTime? Createdat { get; set; }

    public DateTime? Updatedat { get; set; }

    public int? Createdby { get; set; }

    public int? Updatedby { get; set; }

    public virtual User? CreatedbyNavigation { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual ICollection<Ordertoitem> Ordertoitems { get; } = new List<Ordertoitem>();

    public virtual ICollection<Ordertotable> Ordertotables { get; } = new List<Ordertotable>();

    public virtual ICollection<Ordertotax> Ordertotaxes { get; } = new List<Ordertotax>();

    public virtual ICollection<Payment> Payments { get; } = new List<Payment>();

    public virtual ICollection<Rating> Ratings { get; } = new List<Rating>();

    public virtual User? UpdatedbyNavigation { get; set; }
}
