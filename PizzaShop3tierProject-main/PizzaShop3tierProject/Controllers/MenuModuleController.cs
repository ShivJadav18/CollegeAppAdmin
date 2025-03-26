using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NuGet.Common;
using pizzashop.service.Attributes;
using PizzaShop.Repository.Data;
using PizzaShop.Repository.ViewModels;
using PizzaShop.Service.Interfaces;

namespace PizzaShop3tierProject.Controllers;

[CustomAuthorize]
public class MenuModuleController : Controller{

    private readonly IMenuService _menuservice;

    public MenuModuleController(IMenuService menuservice){
        _menuservice = menuservice;
    }

   [CustomAuthorize("4","CanView")]
    public IActionResult ItemAndModifier(){
       var categoriesanditem = GetItemsandCategories();
        return View(categoriesanditem);
    }

    [CustomAuthorize("4","CanDelete")]
    public IActionResult DeleteItem(int itemid){

        Message message = _menuservice.DeleteItemService(itemid);

        if(message.error){
            TempData["error"]  = message.errorMessage;
            return Json( new {success = false});
        }
        TempData["success"] = message.errorMessage;
        return Json(new {success = true});
    }

    [CustomAuthorize("4","CanAddEdit")]
    public IActionResult EditItem(int itemid){

        Item item = _menuservice.GetItemByIdService(itemid);

        if(string.IsNullOrEmpty(item.Name)){
            TempData["error"] = "There is some internal error.";
            return Json(new { success  = false});
        }
        List<Itemtomodifiergroup> itemtomodifiergroups = _menuservice.GetItemtomodifiergroupsByItemIdService(itemid);
        // List<ModifierForAddItem> modifierForAddItems = new List<ModifierForAddItem>{};
        // foreach(Itemtomodifiergroup itemtomodifiergroup in itemtomodifiergroups){

        // }
        List<EditItemModifierGroup> editItemModifierGroups = new List<EditItemModifierGroup>{};
        foreach(Itemtomodifiergroup itemtomodifiergroup in itemtomodifiergroups){
            EditItemModifierGroup editItemModifierGroup = _menuservice.GetGroupandModifierforEditItem((int)itemtomodifiergroup.ModifiergroupId);
            editItemModifierGroup.maxVal = (int)itemtomodifiergroup.Maxval;
            editItemModifierGroup.minVal = (int)itemtomodifiergroup.Minval;
            editItemModifierGroup.groupid = (int)itemtomodifiergroup.ModifiergroupId;
            editItemModifierGroups.Add(editItemModifierGroup);
        }

        return Json(new { success = true, 
        Name = item.Name ,Imageurl = item.Imageurl,Description = item.Description, UnitId = item.UnitId, Itemtype = item.Itemtype, Rate = item.Rate, 
        Quantity = item.Quantity, Defaulttax = item.Defaulttax
        , Taxpercentage = item.Taxpercentage, Isavailable = item.Isavailable, CategoryId = item.CategoryId, Shortcode = item.Shortcode, groups = editItemModifierGroups});
    }

   [CustomAuthorize("4","CanAddEdit")]
    [HttpPost]

    public IActionResult EditItem(NewItem editeditem){
        
        string token = Request.Cookies["jwtCookie"];
        var userid = GetClaimValueHelper(token,"Userid");
        editeditem.Updatedby = Convert.ToInt32(userid);

        Message message = _menuservice.UpdateItemService(editeditem);

        if(message.error){
            TempData["error"] = message.errorMessage;
            return Json(new {success = false, message = message.errorMessage});
        }
        TempData["success"] = "Item is successfully updated.";
        return Json(new {success = true});

    }

    [CustomAuthorize("4","CanAddEdit")]
    [HttpPost]
    public IActionResult AddNewItemAction(NewItem newItem){

        string token = Request.Cookies["jwtCookie"];
        var userid = GetClaimValueHelper(token,"Userid");
        newItem.Createdby = Convert.ToInt32(userid);
        newItem.Updatedby = Convert.ToInt32(userid);
        Message message = _menuservice.AddNewItemService(newItem);

        if(message.error){
            TempData["error"] = message.errorMessage;
            return Json(new{success = false});
        }
        TempData["success"] = "New Item is successfully added";
       return Json(new{success = true});
    }
   
    public IActionResult ItemsForpagination(Items itemsmodel){
        Items itemobj = _menuservice.GetItemsModel(itemsmodel);

        return PartialView("_Items",itemobj);
    }

    [Authorize]
    public IActionResult ForItems(){
        var categoriesanditem = GetItemsandCategories();
        return PartialView("_ItemPartialView",categoriesanditem);
    }

    [Authorize]
    public IActionResult ForModifiers(){

        ModifierGroupandModifier modifierGroupandModifier = _menuservice.GetModifierGroupandModifierService();

        return PartialView("_ModifierPartialView",modifierGroupandModifier);
    }

   [CustomAuthorize("4","CanAddEdit")]
    [HttpPost]
    public IActionResult AddCategory(Category category){
        var token = Request.Cookies["jwtCookie"];
        var userid = GetClaimValueHelper(token,"Userid");
        category.Createdby = Convert.ToInt32(userid);
        category.Updatedby = Convert.ToInt32(userid);
        _menuservice.AddCategoryService(category);
        return Json(new { success = true , message = "New Category is successfully created!"});
    }

    [CustomAuthorize("4","CanAddEdit")]
    public IActionResult EditCategory(int categoryid){

        var categories = _menuservice.GetCategoriesService();

        var category = _menuservice.GetCategoryById(categoryid);
        TempData["categoryid"] = category.CategoryId;
        return Json(new{success = true , categoryid = category.CategoryId, name = category.Categoryname , description = category.Description});

    }

    
    [CustomAuthorize("4","CanAddEdit")]
    [HttpPost]

    public IActionResult EditCategory(string categoryname, string categorydescription){
        int id = Convert.ToInt32(TempData["categoryid"]);
        _menuservice.EditCategoryService(id,categoryname,categorydescription);
        return Json(new {success = true , message = "Category Updated Successfully!"});
    }


    [Authorize]
    [CustomAuthorize("4","CanDelete")]
    public IActionResult DeleteCategory(int id){

        _menuservice.DeleteCategoryService(id);

        return Json(new {success = true, message = "Category successfully deleted."});
    }

    private string GetClaimValueHelper(string token, string claimType){
        var tokenHandler = new JwtSecurityTokenHandler();

        var jwtToken = tokenHandler.ReadJwtToken(token);

        var claim = jwtToken.Claims.FirstOrDefault(c => c.Type == claimType);

        return claim.Value;
    }

    private List<Category> GetCategories(){
        var categories = _menuservice.GetCategoriesService();
        return categories;
    }

    public ItemsandCategories GetItemsandCategories(int categoryid = 0,string searchval = "",int count = 5, int pageno = 1){
        var ItemsandCategories = _menuservice.GetItemsandCategoriesService(categoryid,searchval,count,pageno);
        return ItemsandCategories;
    }

    [CustomAuthorize("4","CanDelete")]
    [HttpPost]
    public IActionResult DeleteMultipleItems ([FromBody] DeleteItemsViewModel ids){
        
        if(ids.ids == null || !ids.ids.Any()){
            TempData["error"] = "You have not selected any item.";
            return Json( new {success = false });
        }

        Message message = _menuservice.DeleteMultipleItemsService(ids.ids);

        if(message.error){
            TempData["error"] = message.errorMessage;
            return Json(new {success = false});
        }

        TempData["success"] = message.errorMessage;
        return Json(new {success = true});

    }

    [Authorize]
    public IActionResult GetModifiersFunction(int groupid,int count,int pageno,string searchval = ""){
        Modifiers modifiersModel = _menuservice.GetModifiersModel(groupid,count,pageno,searchval);

        return PartialView("_modifiers",modifiersModel);
    }

    [CustomAuthorize("4","CanAddEdit")]
    [HttpPost]
    public IActionResult AddModifierGroup([FromBody] ModifierGroupViewModel modifierGroupViewModel){

        string token = Request.Cookies["jwtCookie"];
        var id = GetClaimValueHelper(token,"Userid");
        modifierGroupViewModel.Createdby = Convert.ToInt32(id);
        modifierGroupViewModel.Updatedby = Convert.ToInt32(id);

        Message message = _menuservice.AddModifierGroupService(modifierGroupViewModel);

        if(message.error){
            TempData["error"] = message.errorMessage;
            return Json(new {success = false});
        }
        TempData["success"] = "Modifier Group is Added Successfully";
         return Json(new {success = true});
    }

    [CustomAuthorize("4","CanAddEdit")]
    [HttpPost]
    public IActionResult AddModifier(ModifierViewModel modifierViewModel){

        string token = Request.Cookies["jwtCookie"];
        var id = GetClaimValueHelper(token,"Userid");
        modifierViewModel.Createdby = Convert.ToInt32(id);
        modifierViewModel.Updatedby = Convert.ToInt32(id);
        Message message = _menuservice.AddModifierService(modifierViewModel);

        if(message.error){
            TempData["error"] = message.errorMessage;
            return Json(new { success = false});
        }

        TempData["success"] = "Modifier is successfully Added.";
        return Json(new { success = true });

    }

    [CustomAuthorize("4","CanAddEdit")]
    public IActionResult EditModifier(int modifierid){
        Modifier modifier = _menuservice.GetModifierByIdService(modifierid);

        if(modifier.ModifierId == null) {
            return Json(new {success = false , message = "There is some internal error."});
        }

        return Json(new {success = true, name = modifier.Modifiername ,ModifierId = modifierid , Description = modifier.Description , Rate = modifier.Rate , Quantity = modifier.Quantity, UnitId = modifier.UnitId});
    }

    [CustomAuthorize("4","CanAddEdit")]
    [HttpPost]
    public IActionResult EditModifier(ModifierViewModel modifierViewModel){

        string token = Request.Cookies["jwtCookie"];
        var id = GetClaimValueHelper(token,"Userid");

        modifierViewModel.Updatedby = Convert.ToInt32(id);

        Message message = _menuservice.EditModifierService(modifierViewModel);

        if(message.error){
            TempData["error"] = message.errorMessage;
            return Json(new { success = false });
        }
        TempData["success"] = "Modifier is Successfully Updated.";
        return Json(new {success = true });

    }

    [CustomAuthorize("4","CanDelete")]
    public IActionResult RemoveModifier(int modifierid , int groupid){
        Message message = _menuservice.RemoveModifierService(modifierid,groupid);

        if(message.error){
            TempData["error"] = message.errorMessage;
            return Json(new { success = false });
        }

        TempData["success"] = "Modifier is Successfully Deleted.";
        return Json(new {success = true });
    }

    [Authorize]
    public IActionResult PaginationForExistingModifiers(Modifiers modifiers){

        Modifiers modifiers1 = _menuservice.GetModifiersViewModelForExistingModifiers(modifiers);

        // if(modifiers.flag == true){
        //     return PartialView("_ExistingModifiersForEdit",modifiers1);
        // }

        return PartialView("_ExistingModifiers",modifiers1);

    }

    [Authorize]
    public IActionResult PaginationForExistingModifiersInEdit(Modifiers modifiers){

        Modifiers modifiers1 = _menuservice.GetModifiersViewModelForExistingModifiers(modifiers);

        // if(modifiers.flag == true){
        //     return PartialView("_ExistingModifiersForEdit",modifiers1);
        // }

        return PartialView("_ExistingModifersForEdit",modifiers1);

    }

    [CustomAuthorize("4","CanDelete")]
    public IActionResult RemoveModifierGroup(int modifiergroupid){

        Message message = _menuservice.RemoveModifierGroupService(modifiergroupid);

        if(message.error){
            TempData["error"] = message.errorMessage;
            return Json(new { success = false });
        }

        TempData["success"] = "Modifier Group is successfully deleted.";
        return Json(new { success = true });
    }

    [CustomAuthorize("4","CanDelete")]

    public IActionResult DeleteMultipleModifiers([FromBody] DeleteItemsViewModel deleteItemsViewModel){

        if(!deleteItemsViewModel.ids.Any()){
            TempData["error"] = "You have not selected any modifier.";
            return Json(new {success = false});
        }

        Message message = _menuservice.RemoveMultipleModifiersService(deleteItemsViewModel.ids);

        if(message.error){
            TempData["error"] = message.errorMessage;
            return Json(new {success = false});
        }

        TempData["success"] = "Modifiers are successfully deleted.";
        return Json(new {success = true});

    }

    [CustomAuthorize("4","CanAddEdit")]
    public IActionResult EditModifierGroup(int groupid){

        ModifiergroupEditViewModel modifiergroup = _menuservice.GetModifiergroupByIdService(groupid);

        if(modifiergroup.modifiers == null){
            TempData["error"] = "There is some internal error.";
            return Json( new { success = false });
        }
        List<string> names = new List<string>{};
        List<int> ids = new List<int>{};
        foreach(Modifier modifier in modifiergroup.modifiers){
            names.Add(modifier.Modifiername);
            ids.Add(modifier.ModifierId);
        }

        return Json( new {success = true , name = modifiergroup.Groupname , Description = modifiergroup.Description , modifiersname = names , ids = ids });

    }

    [CustomAuthorize("4","CanAddEdit")]
    [HttpPost]

    public IActionResult EditModifierGroup([FromBody] ModifierGroupViewModel modifierGroupViewModel){
        
        Message message = _menuservice.EditModifierGroupService(modifierGroupViewModel);

        if(message.error){
            TempData["error"] = message.errorMessage;
            return Json( new{ success = false });
        }

        TempData["success"] = "Modifier Group is Updated Successfully.";
        return Json( new { success = true });
    }

    [Authorize] 

    public IActionResult GetModifierForItemAdd(int groupid){

        List<Modifier> modifiers = _menuservice.GetModifiersForItemAddService(groupid);
        List<ModifierForAddItem> modifierForAddItems = new List<ModifierForAddItem>{};

        if( !modifiers.Any() || modifiers[0] == null ){
            return Json(new { success = true , modifiers = modifierForAddItems });
        }

        foreach(Modifier modifier in modifiers){
            ModifierForAddItem modifierForAddItem = new ModifierForAddItem { modifierId = modifier.ModifierId , Rate = modifier.Rate , modifiername = modifier.Modifiername};
            modifierForAddItems.Add(modifierForAddItem);
        }
        return Json(new { success = true , modifiers = modifierForAddItems });

    }
}