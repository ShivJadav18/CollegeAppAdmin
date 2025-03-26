
using PizzaShop.Repository.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using pizzashop.service.Attributes;


namespace PizzaShop3tierProject.Controllers{

    [CustomAuthorize]
    public class DashboardController : Controller {

        private readonly PizzaShopDbContext _context;

        public DashboardController(PizzaShopDbContext context){

            _context = context;

        }

        public async Task<IActionResult> Dashboardpage(){

            var token = Request.Cookies["jwtCookie"];
            if(token != null){
                return View();
            }

            return RedirectToAction("Login","Authenticate");

        }

    }

}