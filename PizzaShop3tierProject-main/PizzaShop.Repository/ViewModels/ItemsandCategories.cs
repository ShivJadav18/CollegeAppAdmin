using PizzaShop.Repository.Data;

namespace PizzaShop.Repository.ViewModels;

public class ItemsandCategories{

    public NewItem newItem{get; set;}
    public List<Category> categories{get; set;}

    public List<Modifiergroup> modifiergroups{get;set;}
    public Items itemmodel{get; set;}

}