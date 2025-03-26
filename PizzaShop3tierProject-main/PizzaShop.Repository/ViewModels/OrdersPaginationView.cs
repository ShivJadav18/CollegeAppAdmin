namespace PizzaShop.Repository.ViewModels;

public class OrdersPaginationView{
    public List<OrdersElementView> ordersElementViews{get;set;}
    public int searchval {get;set;}
    public int count{get;set;}
    public int totalorders{get;set;}
    public int pageno {get;set;}
    public string status{get;set;}
    public string timefil{get;set;}
    public DateTime? from{get;set;}
    public DateTime? todate{get;set;}
    public int order{get;set;}
    public int sortmethod{get;set;}
}