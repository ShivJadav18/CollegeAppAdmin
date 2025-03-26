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

      public List<Modifiergroup> GetModifierGroups();

    public List<Modifier> GetModifiersFromGroupIds(int ModifiergroupId,string searchval);

    public Message AddModifierGroup(ModifierGroupViewModel modifierGroupViewModel);

    public Message AddModifier(Modifier modifier,int groupid);

    public Message AddInModifierToGroup(int modifierid, int groupid);

    public Message UpdateModifier(ModifierViewModel modifierViewModel);
    public Modifier GetModifierById(int modifierid);

    public Message RemoveModifier(int modifierid);

    public List<Modifier> GetAllModifiers(string searchval);

    public Message RemoveModifierGroup(int modifiergroupid);

    public Modifiergroup GetModifiergroupById(int modifiergroupid);

    public Message EditModifierGroup(ModifierGroupViewModel modifierGroupViewModel);

    public Message AddInItemToGroup(List<Itemtomodifiergroup> itemtomodifiergroups);

    public List<Itemtomodifiergroup> GetItemtomodifiergroupsByItemId(int itemid);

    public Message EditInItemToGroup(List<Itemtomodifiergroup> itemtomodifiergroups , int itemid);

    public Message DeleteInItemToGroupByitemId(int itemid);
}