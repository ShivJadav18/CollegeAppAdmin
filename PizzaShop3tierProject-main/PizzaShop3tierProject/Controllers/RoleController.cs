using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using pizzashop.service.Attributes;
using PizzaShop.Repository.Data;
using PizzaShop.Repository.ViewModels;
using PizzaShop.Service.Interfaces;

namespace PizzaShop3tierProject.Controllers;
[CustomAuthorize]
 public class RoleController : Controller{

   private IRoleandPermissionServices _roleandpermissionservice;

   public RoleController(IRoleandPermissionServices roleandpermissionservice){
    _roleandpermissionservice = roleandpermissionservice;
   }

    [CustomAuthorize("3","CanView")]
    public IActionResult Roleselect(){

        return View();
    }

    [CustomAuthorize("3","CanView")]
    public IActionResult Permission(int id){

        Role role = _roleandpermissionservice.GetRoleByIdService(id);

        TempData["RoleName"] = role.Name;

        var permissions = _roleandpermissionservice.GetPermissionsByRoleService(role.RoleId);

        return View(permissions);
    }
    // [CustomAuthorize]
    [CustomAuthorize("3","CanAddEdit")]
    [HttpPost]
    public IActionResult Permission(IEnumerable<Permission> permissions){
        string token = Request.Cookies["jwtCookie"];
        int id = Convert.ToInt32(GetClaimValueHelper(token,"Userid"));

       Message message =  _roleandpermissionservice.UpdatePermissionsService(permissions,id);

        if(message.error){
            TempData["error"] = message.errorMessage;
        }

        return RedirectToAction("Roleselect","Role");
    }

    private string GetClaimValueHelper(string token, string claimType){
        var tokenHandler = new JwtSecurityTokenHandler();

        var jwtToken = tokenHandler.ReadJwtToken(token);

        var claim = jwtToken.Claims.FirstOrDefault(c => c.Type == claimType);

        return claim.Value;
    }

}