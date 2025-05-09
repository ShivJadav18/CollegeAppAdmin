using PizzaShop.Repository.ViewModels;
using PizzaShop.Repository.Data;

namespace PizzaShop.Repository.Interfaces;

public interface IMenu{
    public void AddCategory(Category category);

    public List<Category> GetCategories();

     public void UpdateCategory(Category category);

     public void RemoveCategory(Category category);

      public List<Item> GetItems(int categoryid,string searchval);

      public Message AddItem(Item item);

      public Item GetItem(int itemid);

      public Message UpdateItem(NewItem item);

      public Message DeleteItem(int itemid);

      public Message DeleteMultipleItems(List<int> ids);

     
}