using Microsoft.CodeAnalysis.Differencing;
using PizzaShop.Repository.Data;
using PizzaShop.Repository.Interfaces;
using PizzaShop.Repository.ViewModels;
using PizzaShop.Service.Interfaces;

namespace PizzaShop.Service.Implementation;

public class MenuService : IMenuService
{

    private readonly IMenu _menu;

    public MenuService(IMenu menu)
    {
        _menu = menu;
    }


    public void AddCategoryService(Category category)
    {

        _menu.AddCategory(category);

    }

    public List<Category> GetCategoriesService()
    {
        var categories = _menu.GetCategories();
        return categories;
    }

    public Item GetItemByIdService(int itemid)
    {

        Item item = _menu.GetItem(itemid);

        if (string.IsNullOrEmpty(item.Name))
        {
            return new Item { };
        }
        return item;

    }

    public List<Itemtomodifiergroup> GetItemtomodifiergroupsByItemIdService(int itemid)
    {

        List<Itemtomodifiergroup> itemtomodifiergroups = _menu.GetItemtomodifiergroupsByItemId(itemid);

        return itemtomodifiergroups;

    }

    public Message DeleteItemService(int itemid)
    {
        Message message = _menu.DeleteItem(itemid);

        return message;
    }

    public Message UpdateItemService(NewItem editeditem)
    {

        if (editeditem.ItemImage != null)
        {

            var fileName = Path.GetFileNameWithoutExtension(editeditem.ItemImage.FileName);
            var extension = Path.GetExtension(editeditem.ItemImage.FileName);
            var uniqueFileName = $"{fileName}_{Guid.NewGuid()}{extension}";

            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/UploadedImages");
            var path = Path.Combine(uploadsFolder, uniqueFileName);

            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                editeditem.ItemImage.CopyTo(fileStream);
            }

            // Save the relative path to the usertemp property
            editeditem.Imageurl = $"UploadedImages/{uniqueFileName}";
        }
        editeditem.Updatedat = DateTime.Now;
        Message message = _menu.UpdateItem(editeditem);

        // if (message.error)
        // {
        //     return message;
        // }
        // List<Itemtomodifiergroup> itemtomodifiergroups = new List<Itemtomodifiergroup> { };
        //  Message message1 = new Message{};
        // if(editeditem.Groups == null){
        //       message1 = _menu.EditInItemToGroup(itemtomodifiergroups,(int)editeditem.itemid);
        // }else{
        
        // foreach (ModifierGroupAndMinMax modifierGroupAndMinMax in editeditem.Groups)
        // {
        //     Itemtomodifiergroup itemtomodifiergroup = new Itemtomodifiergroup
        //     {
        //         Minval = modifierGroupAndMinMax.minVal,
        //         Maxval = modifierGroupAndMinMax.maxVal,
        //         ItemId = editeditem.itemid,
        //         ModifiergroupId = modifierGroupAndMinMax.groupid
        //     };
        //     itemtomodifiergroups.Add(itemtomodifiergroup);
        // }
        //  message1 = _menu.EditInItemToGroup(itemtomodifiergroups,(int)editeditem.itemid);
        // }

        // if (message1.error)
        // {
        //     return message1;
        // }

        return message;

    }

    public Items GetItemsModel(Items items)
    {
        var totalitemslist = _menu.GetItems(items.categoryid, items.searchval);
        var totalitems = totalitemslist.Count();

        var itemslist = totalitemslist.Skip((items.pageno - 1) * items.count).Take(items.count).ToList();

        var ItemsObj = new Items
        {
            totalitems = totalitems,
            items = itemslist,
            count = items.count,
            pageno = items.pageno,
            categoryid = items.categoryid,
            searchval = items.searchval
        };

        return ItemsObj;
    }

    public Message AddNewItemService(NewItem newItem)
    {

        if (newItem.ItemImage != null)
        {
            var fileName = Path.GetFileNameWithoutExtension(newItem.ItemImage.FileName);
            var extension = Path.GetExtension(newItem.ItemImage.FileName);
            var uniqueFileName = $"{fileName}_{Guid.NewGuid()}{extension}";

            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/UploadedImages");
            var path = Path.Combine(uploadsFolder, uniqueFileName);

            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                newItem.ItemImage.CopyTo(fileStream);
            }

            // Save the relative path to the newItem property
            newItem.Imageurl = $"UploadedImages/{uniqueFileName}";
        }

        Item item = new Item
        {
            Name = newItem.Name,
            CategoryId = newItem.CategoryId,
            Description = newItem.Description,
            UnitId = newItem.UnitId,
            Imageurl = newItem.Imageurl,
            Itemtype = newItem.Itemtype,
            Rate = newItem.Rate,
            Taxpercentage = newItem.Taxpercentage,
            Defaulttax = newItem.Defaulttax,
            Quantity = newItem.Quantity,
            Shortcode = newItem.Shortcode,
            Isavailable = newItem.Isavailable,
            Createdby = newItem.Createdby,
            Updatedby = newItem.Updatedby,
        };


        Message message = _menu.AddItem(item);

        // if (message.error)
        // {
        //     return message;
        // }
        // List<Itemtomodifiergroup> itemtomodifiergroups = new List<Itemtomodifiergroup> { };
        // foreach (ModifierGroupAndMinMax modifierGroupAndMinMax in newItem.Groups)
        // {
        //     Itemtomodifiergroup itemtomodifiergroup = new Itemtomodifiergroup
        //     {
        //         Minval = modifierGroupAndMinMax.minVal,
        //         Maxval = modifierGroupAndMinMax.maxVal,
        //         ItemId = item.ItemId,
        //         ModifiergroupId = modifierGroupAndMinMax.groupid
        //     };
        //     itemtomodifiergroups.Add(itemtomodifiergroup);
        // }
        // Message message1 = _menu.AddInItemToGroup(itemtomodifiergroups);

        // if (message1.error)
        // {
        //     return message1;
        // }

        return message;
    }

    public ItemsandCategories GetItemsandCategoriesService(int categoryid, string searchval = "", int count = 5, int pageno = 1)
    {

        var categories = GetCategoriesService();
        categoryid = categories[0].CategoryId;
        var itemobj = new Items
        {
            categoryid = categoryid,
            searchval = searchval,
            count = count,
            pageno = pageno
        };
        var itemmodel = GetItemsModel(itemobj);
        List<Modifiergroup> modifiergroups = _menu.GetModifierGroups();

        ItemsandCategories ItemsandCategories = new ItemsandCategories
        {
            categories = categories,
            itemmodel = itemmodel,
            modifiergroups = modifiergroups
        };

        return ItemsandCategories;
    }

    public Category GetCategoryById(int categoryid)
    {
        var categories = _menu.GetCategories();

        var category = categories.FirstOrDefault(c => c.CategoryId == categoryid);

        return category;
    }

    public void DeleteCategoryService(int id)
    {
        var categories = _menu.GetCategories();

        var category = categories.FirstOrDefault(c => c.CategoryId == id);

        _menu.RemoveCategory(category);
    }

    public void EditCategoryService(int id, string name, string description)
    {

        var categories = _menu.GetCategories();

        var category = categories.FirstOrDefault(c => c.CategoryId == id);

        category.Categoryname = name;
        category.Description = description;

        _menu.UpdateCategory(category);

    }

    public Message DeleteMultipleItemsService(List<int> ids)
    {

        Message message = _menu.DeleteMultipleItems(ids);

        return message;

    }

    public Modifiers GetModifiersModel(int groupid, int count, int pageno, string searchval = "")
    {

        List<Modifier> modifiers = _menu.GetModifiersFromGroupIds(groupid, searchval);

        List<Modifier> finalModifiers = modifiers.Skip((pageno - 1) * count).Take(count).ToList();

        Modifiers modifiers1 = new Modifiers
        {
            modifiers = finalModifiers,
            count = count,
            pageno = pageno,
            totalmodifiers = modifiers.Count(),
            groupid = groupid
        };

        return modifiers1;

    }

    public ModifierGroupandModifier GetModifierGroupandModifierService()
    {

        List<Modifiergroup> modifiergroups = _menu.GetModifierGroups();

        if (modifiergroups == null || modifiergroups.Count() == 0)
        {
            return new ModifierGroupandModifier
            {
                modifiergroups = modifiergroups
            };
        }
        Modifiers modifiers2 = GetModifiersModel(modifiergroups[0].ModifiergroupId, 5, 1, "");
        Modifiers temp = new Modifiers
        {
            count = 5,
            searchval = "",
            pageno = 1,
        };
        Modifiers modifiers3 = GetModifiersViewModelForExistingModifiers(temp);
        ModifierGroupandModifier modifierGroupandModifiers = new ModifierGroupandModifier
        {
            modifiers1 = modifiers2,
            modifiers2 = modifiers3,
            modifiergroups = modifiergroups
        };

        return modifierGroupandModifiers;
    }

    public Message AddModifierGroupService(ModifierGroupViewModel modifierGroupViewModel)
    {

        Message message = _menu.AddModifierGroup(modifierGroupViewModel);

        return message;
    }

    public Message AddModifierService(ModifierViewModel modifierViewModel)
    {

        Modifier modifier = new Modifier
        {
            Modifiername = modifierViewModel.Modifiername,
            Description = modifierViewModel.Description,
            Rate = modifierViewModel.Rate,
            Quantity = modifierViewModel.Quantity,
            UnitId = modifierViewModel.UnitId,
            Createdby = (int)modifierViewModel.Createdby,
            Updatedby = (int)modifierViewModel.Updatedby
        };



        Message message = _menu.AddModifier(modifier, modifierViewModel.GroupId);

        return message;
    }

    public Modifier GetModifierByIdService(int modifierid)
    {

        Modifier modifier = _menu.GetModifierById(modifierid);

        return modifier;

    }

    public Message EditModifierService(ModifierViewModel modifierViewModel)
    {

        modifierViewModel.Updatedat = DateTime.Now;

        Message message = _menu.UpdateModifier(modifierViewModel);

        return message;
    }

    public Message RemoveModifierService(int modifierid, int groupid)
    {

        Message message = _menu.RemoveModifier(modifierid);

        return message;
    }

    public Modifiers GetModifiersViewModelForExistingModifiers(Modifiers modifiers)
    {

        List<Modifier> modifiers1 = _menu.GetAllModifiers(modifiers.searchval);

        List<Modifier> finalModifiers = modifiers1.Skip((modifiers.pageno - 1) * modifiers.count).Take(modifiers.count).ToList();

        Modifiers modifiers2 = new Modifiers
        {
            count = modifiers.count,
            totalmodifiers = modifiers1.Count(),
            modifiers = finalModifiers,
            searchval = modifiers.searchval,
            pageno = modifiers.pageno
        };

        return modifiers2;

    }

    public Message RemoveModifierGroupService(int modifiergroupid)
    {

        Message message = _menu.RemoveModifierGroup(modifiergroupid);

        return message;
    }

    public Message RemoveMultipleModifiersService(List<int> ids)
    {

        foreach (int id in ids)
        {
            Message message = _menu.RemoveModifier(id);
            if (message.error)
            {
                return message;
            }
        }
        return new Message { error = false };
    }

    public ModifiergroupEditViewModel GetModifiergroupByIdService(int groupid)
    {

        Modifiergroup modifiergroup = _menu.GetModifiergroupById(groupid);

        List<Modifier> modifiers = _menu.GetModifiersFromGroupIds(groupid, "");

        return new ModifiergroupEditViewModel { Groupname = modifiergroup.Groupname, Description = modifiergroup.Description, modifiers = modifiers };
    }

    public Message EditModifierGroupService(ModifierGroupViewModel modifierGroupViewModel)
    {

        Message message = _menu.EditModifierGroup(modifierGroupViewModel);

        return message;
    }

    public List<Modifier> GetModifiersForItemAddService(int groupid)
    {
        List<Modifier> modifiers = _menu.GetModifiersFromGroupIds(groupid, "");

        return modifiers;
    }

    public EditItemModifierGroup GetGroupandModifierforEditItem(int groupid)
    {

        List<Modifier> modifiers = _menu.GetModifiersFromGroupIds(groupid, "");
        List<ModifierForAddItem> modifierForAddItems = new List<ModifierForAddItem> { };
        foreach (Modifier modifier in modifiers)
        {
            ModifierForAddItem modifierForAddItem = new ModifierForAddItem { modifierId = modifier.ModifierId, modifiername = modifier.Modifiername, Rate = modifier.Rate };
            modifierForAddItems.Add(modifierForAddItem);
        }

        return new EditItemModifierGroup { modifiers = modifierForAddItems };

    }

}