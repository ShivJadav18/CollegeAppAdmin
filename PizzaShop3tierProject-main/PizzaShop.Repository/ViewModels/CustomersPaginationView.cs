using PizzaShop.Repository.Data;

namespace PizzaShop.Repository.ViewModels;

public class CustomersPaginationView{
    public List<CustomerView> customers{get;set;}
    public int totalCustomers{get;set;}
    public int pageno{get;set;}
    public int count{get;set;}
    public string searchval{get;set;}
    public string timefilter{get;set;}
    public int sortval{get;set;}
    public int sortmethod{get;set;}
    public DateTime from{get;set;}
    public DateTime to{get;set;}
}

public class CustomerView{
    public int CustomerId { get; set; }
    public string Firstname { get; set; } = null!;
    public string Contactnumber { get; set; } = null!;
    public string? Email { get; set; }
    public int totalOrders{ get; set; }
    public DateTime OrderDate{ get; set; }
}