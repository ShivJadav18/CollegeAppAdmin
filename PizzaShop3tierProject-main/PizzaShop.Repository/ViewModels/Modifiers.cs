using PizzaShop.Repository.Data;

namespace PizzaShop.Repository.ViewModels;

public class Modifiers{

    public List<Modifier> modifiers{get; set;}

    public int count{get; set;}
    public int totalmodifiers{get; set;}
    public int pageno{get;set;}
    public int groupid{get; set;}
    public string searchval{get;set;}

    // public bool flag{get;set;}
}