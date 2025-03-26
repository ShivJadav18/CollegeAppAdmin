using System;
using System.Collections.Generic;

namespace PizzaShop.Repository.Data;

public partial class Favouriteitem
{
    public int FavouriteitemId { get; set; }

    public int? ItemId { get; set; }

    public virtual Item? Item { get; set; }
}
