using Microsoft.AspNetCore.Mvc;
using PizzaShop.Repository.ViewModels;
using PizzaShop.Service.Implementation;

namespace PizzaShop.Service.Interfaces;

public interface IAuthenticate{

    public string GetAuthenticate(Userlogin user);

    public  bool SendEmailForResetpass(string email,string baseUrl,string token = "");

    public bool ResetPassService(string email,string password,bool isForChangePassstring=false,string Currentpass="");

    public Message ResetPassValidationService(string email);
     public Message TokenValidationForResetPass(string token);
     public string GetUserFromResetToken(string token);
     public Message SendEmailTONewUserService(string email,string username,string baseUrl,string password);
    // public string GetValueFromToken(string token, string claimType);
}