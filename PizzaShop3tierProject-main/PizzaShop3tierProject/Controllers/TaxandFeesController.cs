using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Mvc;
using pizzashop.service.Attributes;
using PizzaShop.Repository.Data;
using PizzaShop.Repository.ViewModels;
using PizzaShop.Service.Interfaces;

namespace PizzaShop3tierProject.Controllers;

[CustomAuthorize]
public class TaxandFeesController : Controller{

    private readonly ITaxServices _taxServices;

    public TaxandFeesController(ITaxServices taxServices){
        _taxServices = taxServices;
    }

    [CustomAuthorize("5","CanView")]
    public IActionResult TaxandFeespage(){
        List<Tax> taxes = _taxServices.GetTaxesService();
        TaxandFeesViewModel taxandFeesViewModel = new TaxandFeesViewModel{
            taxes = taxes
        };
        return View(taxandFeesViewModel);
    }

    [CustomAuthorize("5","CanAddEdit")]
    public IActionResult AddTax(Tax tax){
        string token = Request.Cookies["jwtCookie"];
        int id = Convert.ToInt32(GetClaimValueHelper(token,"Userid"));
        tax.Createdby = id;
        tax.Updatedby = id;

        Message message = _taxServices.AddTaxService(tax);
        if(message.error){
            TempData["error"] = message.errorMessage;
            return Json(new{ success = false });
        }
        TempData["success"] = "Tax is successfully added.";
        return Json(new{success = true });
    }

    [CustomAuthorize("5","CanAddEdit")]
    public IActionResult EditTax(int taxid){
        Tax tax = _taxServices.GetTaxForEdit(taxid);
        return Json( new {success = true ,name = tax.Name , amount = tax.Amount , type = tax.Taxtype , isdefault = tax.Isdefault , isenable = tax.Isenabled});
    }

    [CustomAuthorize("5","CanAddEdit")]
    [HttpPost]
    public IActionResult EditTax(EditTaxViewModel editedTax){
        string token = Request.Cookies["jwtCookie"];
        int id = Convert.ToInt32(GetClaimValueHelper(token,"Userid"));

        editedTax.Updatedby = id;
        Message message = _taxServices.EditTaxService(editedTax);

        if(message.error){
            TempData["error"] = message.errorMessage;
            return Json(new{ success = false });
        }

        TempData["success"] = "Tax is successfully Updated.";
        return Json(new{success = true });
    }

    [CustomAuthorize("5","CanDelete")]
    public IActionResult DeleteTax(int taxid){
        Message message  = _taxServices.DeleteTaxService(taxid);

        if(message.error){
            TempData["error"] = message.errorMessage;
            return Json(new{ success = false });
        }

        TempData["success"] = "Tax is successfully Deleted.";
        return Json(new{success = true });

    }

    private string GetClaimValueHelper(string token, string claimType){
        var tokenHandler = new JwtSecurityTokenHandler();

        var jwtToken = tokenHandler.ReadJwtToken(token);

        var claim = jwtToken.Claims.FirstOrDefault(c => c.Type == claimType);

        return claim.Value;
    }
}