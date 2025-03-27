namespace PizzaShop.Repository.ViewModels;

public class ItemDetailForOrder{
    public int ordertoitemid{get;set;}
    public string itemName{get;set;}
    public decimal itemAmount{get;set;}
    public int itemQuantity{get;set;}
    public decimal unitAmount{get;set;}
    // public List<ModifierDetailForOrder> itemModifiers{get;set;}
    public decimal totalAmountForThisOne {get;set;}
}