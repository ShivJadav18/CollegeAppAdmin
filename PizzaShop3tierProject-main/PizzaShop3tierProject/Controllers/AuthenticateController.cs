using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PizzaShop.Repository.ViewModels;
using PizzaShop.Service.Implementation;
using PizzaShop.Service.Interfaces;
using PizzaShop3tierProject.Models;

namespace PizzaShop3tierProject.Controllers;

public class AuthenticateController : Controller
{
    private readonly ILogger<AuthenticateController> _logger;
    private readonly IAuthenticate _userauth;

    public AuthenticateController(ILogger<AuthenticateController> logger, IAuthenticate userauth)
    {
        _logger = logger;
        _userauth = userauth;
    }

    public IActionResult Login()
    {
        var token = Request.Cookies["jwtCookie"];
        if (token != null)
        {
            return RedirectToAction("Dashboardpage", "Dashboard");
        }

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(Userlogin user)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }
        var jwtToken = _userauth.GetAuthenticate(user);
        if (jwtToken == "")
        {
            TempData["error"] = "Email/Password is Wrong Or This user's status is in-active or this user is deleted.";
            return View();
        }
        if (user.rememberme)
        {
            SetJWTCookie(jwtToken, 7, "jwtCookie");
        }
        else
        {
            SetJWTCookie(jwtToken, 1, "jwtCookie");
        }
        TempData["success"] = "You are Successfully Logged In!";
        return RedirectToAction("Dashboardpage", "Dashboard");
    }

    public IActionResult Forgotpage()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Forgotpage(string email)
    {
        // storing the email id in tempdata.
       Message message = _userauth.ResetPassValidationService(email);

        if(message.error){
            TempData["error"] = message.errorMessage;
             return Json(new { success = false});
        }
        string token = message.errorMessage;
        var baseUrl = $"{Request.Scheme}://{Request.Host}";

        bool result = _userauth.SendEmailForResetpass(email, baseUrl,token);
        if (result)
        {
            TempData["success"] = "Email For Reset Password is send.";
            return Json(new { success = true });
        }
        else
        {
            TempData["error"] = "Entered Email is not correct.";
            return Json(new { success = false, message = "Please Enter Right Email!" });
        }
    }

    public async Task<IActionResult> Resetpage()
    {
        string token = Request.Query["token"];
        Message message = _userauth.TokenValidationForResetPass(token);

        if(message.error){
            TempData["error"] = message.errorMessage;
            return RedirectToAction("Forgotpage","Authenticate");
        }


        return View();
    }


    [HttpPost]
    public async Task<IActionResult> Resetpage(string password,string Cnfpass)
    {
        if(password != Cnfpass){
            TempData["error"] = "The New password and confirm password are not matching";
            return Json(new { success = false, message = "User's Password is Not Changed!" });
        }

        string token = Request.Query["token"];

        string email = _userauth.GetUserFromResetToken(token);

        if(email == ""){
            TempData["error"] = "There is an error in Updating reset token.";
            return Json(new { success = false, message = "There is an error in Updating reset token." });
        }

        bool result = _userauth.ResetPassService(email, password);

        if (result)
        {
            TempData["success"] = "User's Password is successfully changed.";
            return Json(new { success = true, message = "User's Password is successfully changed." });
        }
        else
        {
            TempData["error"] = "User's Password is Not Changed!";
            return Json(new { success = false, message = "User's Password is Not Changed!" });
        }

    }


    [Authorize]
    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    private void SetJWTCookie(string token, int days, string name)
    {
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Expires = DateTime.UtcNow.AddDays(days),
        };
        Response.Cookies.Append(name, token, cookieOptions);
    }
}
