namespace PizzaShop.Repository.ViewModels;

public class EditTaxViewModel{

    public int TaxId { get; set; }

    public string? Name { get; set; }

    public bool Isenabled { get; set; }

    public bool Isdefault { get; set; }

    public string? Taxtype { get; set; }

    public decimal? Amount { get; set; }
    public DateTime? Updatedat { get; set; }
    public int? Updatedby { get; set; }

}