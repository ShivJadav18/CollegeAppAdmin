using System;
using System.Collections.Generic;

namespace PizzaShop.Repository.Data;

public partial class Resettoken
{
    public int TokenId { get; set; }

    public int? UserId { get; set; }

    public string? Token { get; set; }

    public bool? Isreseted { get; set; }

    public DateTime? Createdat { get; set; }

    public DateTime? Expiredat { get; set; }

    public virtual User? User { get; set; }
}
