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
            Console.WriteLine("df;skf;"+uploadsFolder);
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                editeditem.ItemImage.CopyTo(fileStream);
            }

            // Save the relative path to the usertemp property
            editeditem.Imageurl = $"UploadedImages/{uniqueFileName}";
            uploadsFolder = "D:\\CollegeClientSide\\CollegeProj\\ElectroSphereProj\\wwwroot\\UploadedImages"; 
           
             Console.WriteLine("df;skf;2"+uploadsFolder);
            path = Path.Combine(uploadsFolder, uniqueFileName);

            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                editeditem.ItemImage.CopyTo(fileStream);
            }
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
            Typeid = newItem.TypeId,
            Imageurl = newItem.Imageurl,
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

        ItemsandCategories ItemsandCategories = new ItemsandCategories
        {
            categories = categories,
            itemmodel = itemmodel,
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

}