namespace PizzaShop.Repository.ViewModels;

public class OrderDetailsView{
    public int orderid{get;set;}
    public string customerName{get;set;}
    public string contactNumber{get;set;}
    public string customerEmail{get;set;}
    public int Noofperson{get;set;}
    public string section{get;set;}
    public string table{get;set;}
    public DateTime orderDate{get;set;}
    public DateTime modifiedDate{get;set;}
    public decimal subTotal{get;set;}
    public decimal Total{get;set;}
    public List<ItemDetailForOrder> itemsInOrder{get;set;}
    public List<TaxForOrder> taxesForOrder{get;set;}
    public string paymentMethod{get;set;}
    public string paymentStatus{get;set;}
}