using System;
using System.Collections.Generic;

namespace PizzaShop.Repository.Data;

public partial class Payment
{
    public int PaymentId { get; set; }

    public int? OrderId { get; set; }

    public string? Paymentmethod { get; set; }

    public string? Paymentstatus { get; set; }

    public decimal? Amount { get; set; }

    public DateTime? Createdat { get; set; }

    public DateTime? Updatedat { get; set; }

    public virtual Order? Order { get; set; }
}
