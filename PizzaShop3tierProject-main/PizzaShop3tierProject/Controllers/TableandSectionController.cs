using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using pizzashop.service.Attributes;
using PizzaShop.Repository.Data;
using PizzaShop.Repository.ViewModels;
using PizzaShop.Service.Interfaces;

namespace PizzaShop3tierProject.Controllers;

[CustomAuthorize]
public class TableandSectionController : Controller{

    private  ITableandSectionService _tableandsectionservice ;

    public TableandSectionController ( ITableandSectionService tableandSectionService){
        _tableandsectionservice = tableandSectionService;
    }

    [CustomAuthorize("4","CanView")]
    public IActionResult TableandSectionView(){

        TableandSectionViewModel tableandSectionViewModel = _tableandsectionservice.GetTableandSectionViewModel();

        if(!tableandSectionViewModel.sections.Any()){
            TempData["error"] = "Some internal error.";
            return RedirectToAction("Dashboardpage","Dashboard");
        }

        return View(tableandSectionViewModel);
    }

    [CustomAuthorize("4","CanAddEdit")]

    public IActionResult Addsection(string Sectionname,string Description){

        string token = Request.Cookies["jwtCookie"];
        int id = Convert.ToInt32(GetClaimValueHelper(token, "Userid"));
        Message message = _tableandsectionservice.AddSectionService(Sectionname , Description , id);

        if(message.error){
            TempData["error"] = message.errorMessage;
            return Json(new{ success = false});
        }

        TempData["success"] = "Section is Successfully Added.";
        return Json( new { success = true });

    }

     private string GetClaimValueHelper(string token, string claimType){
        var tokenHandler = new JwtSecurityTokenHandler();

        var jwtToken = tokenHandler.ReadJwtToken(token);

        var claim = jwtToken.Claims.FirstOrDefault(c => c.Type == claimType);

        return claim.Value;
    }

    [CustomAuthorize("4","CanAddEdit")]
    public IActionResult EditSection(int sectionid){

        Section section = _tableandsectionservice.GetSectionByIdService(sectionid);

        if(section.SectionId == null){
            TempData["error"] = "There is some internal error";
            return Json( new { success = false });
        }


        return Json( new { success = true , name = section.Name , description = section.Description } );
    }

    [CustomAuthorize("4","CanAddEdit")]
    [HttpPost]
    public IActionResult EditSection(Section section){

        String token = Request.Cookies["jwtCookie"];

        int id = Convert.ToInt32(GetClaimValueHelper(token,"Userid"));

        section.Updatedby = id;

        Message message =_tableandsectionservice.EditSection(section);

        if(message.error){
            TempData["error"] = message.errorMessage;
            return Json( new { success = false });
        }

        TempData["success"] = "Section is successfully Updated.";
        return Json(new { success = true });
    }

    [CustomAuthorize("4","CanDelete")]
    public IActionResult DeleteSection(int sectionid){

        Message message = _tableandsectionservice.DeleteSectionService(sectionid);

        if(message.error){
            TempData["error"] = message.errorMessage;
            return Json(new {success = false});
        }

        TempData["success"] = "Section is Successfully Deleted.";
        return Json(new {success = true});
    }

    [Authorize]
    public IActionResult GetTables(int sectionid ,string searchval , int count , int pageno){

        Tables tables = _tableandsectionservice.GetTables(count,searchval,pageno,sectionid);

        return PartialView("_Tables",tables);
    }

[CustomAuthorize("4","CanDelete")]
    public IActionResult DeleteTable(int tableid){
        Message message = _tableandsectionservice.DeleteTableService(tableid);

        if(message.error == true){
            TempData["error"] = message.errorMessage;
            return Json(new {success =  false});
        }

        TempData["success"] = "This Table is successfully deleted.";
        return Json(new { success = true });
    }

    [CustomAuthorize("4","CanAddEdit")]
    public IActionResult AddTable(NewTable newTable){
        string token = Request.Cookies["jwtCookie"];
        int id = Convert.ToInt32(GetClaimValueHelper(token,"Userid"));
        newTable.Createdby = id;
        newTable.Updatedby = id;

        Message message = _tableandsectionservice.AddTableService(newTable);

        if(message.error){
            TempData["error"] = message.errorMessage;
            return Json(new{success = false});
        }

        TempData["success"] = "New Table is successfully added.";
        return Json(new { success = true });
    }

    [CustomAuthorize("4","CanAddEdit")]
    public IActionResult EditTable (int tableid){

        Table table = _tableandsectionservice.GetTableByIdService(tableid);

        if(table.TableId == null){
            return Json(new{success = false });
        }

        return Json(new {success = true , name = table.Name , sectionid = table.SectionId , capacity = table.Capacity , status = table.Status.ToString() });
    }

    [CustomAuthorize("4","CanAddEdit")]
    [HttpPost]
    public IActionResult EditTable(NewTable editedTable){
        string token = Request.Cookies["jwtCookie"];
        int id = Convert.ToInt32(GetClaimValueHelper(token,"Userid"));

        editedTable.Updatedby = id;
        Message message = _tableandsectionservice.UpdateTable(editedTable);
        if(message.error){
            TempData["error"] = message.errorMessage;
            return Json(new { success = false });
        }

        TempData["success"] = "Table is Successfully Updated.";
        return Json(new {success = true});
    }

   [CustomAuthorize("4","CanDelete")]
    public IActionResult DeleteMultipleTables([FromBody] DeleteItemsViewModel deleteItemsViewModel){

        if(deleteItemsViewModel.ids.Count() == 0){
            TempData["error"] = "You have not selected any tables.";
            return Json(new { success = false });
        }

        Message message = _tableandsectionservice.DeleteMultipleTablesService(deleteItemsViewModel);
        
        if(message.error){
            TempData["error"] = message.errorMessage;
            return Json(new { success = false });
        }

        TempData["success"] = "Tables successfully deleted.";
        return Json(new {success = true});
    }
}