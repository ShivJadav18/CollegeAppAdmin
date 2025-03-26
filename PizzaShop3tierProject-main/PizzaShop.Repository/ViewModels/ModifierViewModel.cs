using System;
using System.Collections.Generic;

namespace PizzaShop.Repository.ViewModels;

public partial class ModifierViewModel
{
    public int ?ModifierId{get; set;}
    public int GroupId {get ; set;}
    public string Modifiername { get; set; } = null!;

    public decimal Rate { get; set; }

    public int UnitId { get; set; }

    public int Quantity { get; set; }

    public string? Description { get; set; }

    public int? Createdby { get; set; }

    public int? Updatedby { get; set; }

    public DateTime? Updatedat {get; set;}

}
