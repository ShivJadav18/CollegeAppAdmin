using PizzaShop.Repository.Data;

namespace PizzaShop.Repository.ViewModels;

public class EditItemModifierGroup{
    
    public List<ModifierForAddItem> modifiers {get;set;}

    public int groupid{get;set;}


    public int minVal{get;set;}

    public int maxVal{get;set;}

}