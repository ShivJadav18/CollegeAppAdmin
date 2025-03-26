using Microsoft.AspNetCore.Mvc;

namespace PizzaShop3tierProject.Controllers;

public class CustomerController : Controller{

    public IActionResult CustomerView(){
        return View();
    }

}