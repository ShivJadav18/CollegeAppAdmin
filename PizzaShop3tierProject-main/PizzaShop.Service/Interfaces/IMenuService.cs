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

     public ModifierGroupandModifier GetModifierGroupandModifierService();

      public Modifiers GetModifiersModel(int groupid,int count,int pageno ,string searchval);

      public Message AddModifierGroupService(ModifierGroupViewModel modifierGroupViewModel);

      public Message AddModifierService(ModifierViewModel modifierViewModel);

      public Modifier GetModifierByIdService(int modifierid);

      public Message EditModifierService(ModifierViewModel modifierViewModel);

      public Message RemoveModifierService(int modifierid, int groupid);

      public Modifiers GetModifiersViewModelForExistingModifiers(Modifiers modifiers);

      public Message RemoveModifierGroupService(int modifiergroupid);

       public Message RemoveMultipleModifiersService(List<int> ids);

       public ModifiergroupEditViewModel GetModifiergroupByIdService(int groupid);

       public Message EditModifierGroupService(ModifierGroupViewModel modifierGroupViewModel);

        public List<Modifier> GetModifiersForItemAddService(int groupid);

        public List<Itemtomodifiergroup> GetItemtomodifiergroupsByItemIdService(int itemid);

        public EditItemModifierGroup GetGroupandModifierforEditItem(int groupid);
}