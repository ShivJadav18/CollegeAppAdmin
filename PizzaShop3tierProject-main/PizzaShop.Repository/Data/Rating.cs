using System;
using System.Collections.Generic;

namespace PizzaShop.Repository.Data;

public partial class Rating
{
    public int RatingId { get; set; }

    public int? OrderId { get; set; }

    public int? Foodrate { get; set; }

    public int? Servicerate { get; set; }

    public int? Ambiencerate { get; set; }

    public string? Comment { get; set; }

    public DateTime? Createdat { get; set; }

    public DateTime? Updatedat { get; set; }

    public int? Createdby { get; set; }

    public int? Updatedby { get; set; }

    public virtual User? CreatedbyNavigation { get; set; }

    public virtual Order? Order { get; set; }

    public virtual User? UpdatedbyNavigation { get; set; }
}
