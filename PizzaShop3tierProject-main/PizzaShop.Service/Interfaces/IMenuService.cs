using PizzaShop.Repository.Data;
using PizzaShop.Repository.ViewModels;

namespace PizzaShop.Service.Interfaces;

public interface IMenuService{
    

    public void AddCategoryService(Category category);

    public List<Category> GetCategoriesService();

    public Category GetCategoryById(int categoryid);

    public void EditCategoryService(int id,string name,string description);

    public void DeleteCategoryService(int id);

    public ItemsandCategories GetItemsandCategoriesService(int categoryid,string searchval = "",int count = 5,int pageno = 1);

     public Items GetItemsModel(Items items);

     public Message AddNewItemService(NewItem newItem);

     public Item GetItemByIdService(int itemid);

     public Message UpdateItemService(NewItem editeditem);

     public Message DeleteItemService(int itemid);

     public Message DeleteMultipleItemsService(List<int> ids);
}