using PizzaShop.Repository.Data;

namespace PizzaShop.Repository.ViewModels;

public class ModifiergroupEditViewModel{

    public string Groupname{get; set;}

    public string Description { get; set; }

    public List<Modifier> modifiers { get; set; }

}