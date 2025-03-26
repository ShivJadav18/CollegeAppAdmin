using System;
using System.Collections.Generic;

namespace PizzaShop.Repository.Data;

public partial class Customer
{
    public int CustomerId { get; set; }

    public string Firstname { get; set; } = null!;

    public string Lastname { get; set; } = null!;

    public string Contactnumber { get; set; } = null!;

    public string? Email { get; set; }

    public DateTime? Createdat { get; set; }

    public DateTime? Updatedat { get; set; }

    public int Createdby { get; set; }

    public int Updatedby { get; set; }

    public bool? Isdeleted { get; set; }

    public virtual User CreatedbyNavigation { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; } = new List<Order>();

    public virtual User UpdatedbyNavigation { get; set; } = null!;
}
