using System;
using System.Collections.Generic;

namespace PizzaShop.Repository.Data;

public partial class Table
{
    public int TableId { get; set; }

    public string Name { get; set; } = null!;

    public int SectionId { get; set; }

    public int Capacity { get; set; }

    public bool? Status { get; set; }

    public DateTime? Createdat { get; set; }

    public DateTime? Updatedat { get; set; }

    public int Createdby { get; set; }

    public int Updatedby { get; set; }

    public bool? Isdeleted { get; set; }

    public virtual User CreatedbyNavigation { get; set; } = null!;

    public virtual ICollection<Ordertotable> Ordertotables { get; } = new List<Ordertotable>();

    public virtual Section Section { get; set; } = null!;

    public virtual User UpdatedbyNavigation { get; set; } = null!;
}
