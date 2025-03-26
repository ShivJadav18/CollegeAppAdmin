using System;
using System.Collections.Generic;

namespace PizzaShop.Repository.Data;

public partial class Section
{
    public int SectionId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime? Createdat { get; set; }

    public DateTime? Updatedat { get; set; }

    public int Createdby { get; set; }

    public int Updatedby { get; set; }

    public bool? Isdeleted { get; set; }

    public virtual User CreatedbyNavigation { get; set; } = null!;

    public virtual ICollection<Table> Tables { get; } = new List<Table>();

    public virtual User UpdatedbyNavigation { get; set; } = null!;

    public virtual ICollection<Waitingtoken> Waitingtokens { get; } = new List<Waitingtoken>();
}
