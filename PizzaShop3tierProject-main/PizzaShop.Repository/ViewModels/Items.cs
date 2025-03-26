using PizzaShop.Repository.Data;

namespace PizzaShop.Repository.ViewModels;

public class Items{

    public List<Item> items {get;set;}
    public int count{get; set;}
    public int totalitems{get; set;}

    public int pageno{get;set;}
    public int categoryid{get; set;}
    public string searchval{get;set;}

}