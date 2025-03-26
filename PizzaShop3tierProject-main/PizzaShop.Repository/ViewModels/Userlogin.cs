
using System.ComponentModel.DataAnnotations;

namespace PizzaShop.Repository.ViewModels{
public  class Userlogin
{
    [Required(ErrorMessage = "Please Enter Valid Email.")]
    [RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}",ErrorMessage ="Please Enter Proper Email.")]
    public string Email{get;set;}

    [Required(ErrorMessage = "Please Enter Valid Password.")]
    public string Password{get;set;}

    public bool rememberme {get; set;}
}

}