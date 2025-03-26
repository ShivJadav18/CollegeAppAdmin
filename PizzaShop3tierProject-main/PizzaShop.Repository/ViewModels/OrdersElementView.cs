namespace PizzaShop.Repository.ViewModels;

public class OrdersElementView{
    public int order_id{get;set;}
    public string customer{get;set;}
    public string payment_mode{get;set;}
    public DateTime date{get;set;}
    public string status{get;set;}
    public int rating{get;set;}
    public decimal total_amount{get;set;}
    
}