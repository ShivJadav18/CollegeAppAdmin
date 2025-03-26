using System;
using System.Collections.Generic;

namespace PizzaShop.Repository.Data;

public partial class Waitingtoken
{
    public int WaitingtokenId { get; set; }

    public int SectionId { get; set; }

    public TimeOnly? Waitingtime { get; set; }

    public string Firstname { get; set; } = null!;

    public string Lastname { get; set; } = null!;

    public string Contactnumber { get; set; } = null!;

    public string? Email { get; set; }

    public int Noofpersons { get; set; }

    public bool? Isassigned { get; set; }

    public DateTime? Createdat { get; set; }

    public DateTime? Updatedat { get; set; }

    public int Createdby { get; set; }

    public int Updatedby { get; set; }

    public virtual User CreatedbyNavigation { get; set; } = null!;

    public virtual Section Section { get; set; } = null!;

    public virtual User UpdatedbyNavigation { get; set; } = null!;
}
