using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1.Iana;

namespace PizzaShop3tierProject.Controllers;

public class ErrorPageController : Controller{

    public IActionResult errorPage(){
        return View();
    }

}