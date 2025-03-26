using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using NuGet.Packaging.Signing;
using PizzaShop.Repository.Data;

namespace PizzaShop.Repository.ViewModels;

public class ProfileViewModel{

    public string Email {get; set;}

    public string ?Rolename {get; set;}
    [Required(ErrorMessage = "The Firstname can not be empty.")]
    [RegularExpression(@"^[a-zA-Z]+",ErrorMessage = "Firstname can only contain characters.")]
    public string Firstname {get; set;}

    [Required(ErrorMessage = "The Lastname can not be empty.")]
    [RegularExpression(@"^[a-zA-Z]+",ErrorMessage = "Lastname can only contain characters.")]
    public string Lastname {get; set;}

    [Required(ErrorMessage = "The Username can not be empty.")]
    [RegularExpression(@"^[a-zA-Z0-9._]+",ErrorMessage = "Username can only contain characters, numbers and . or _ .")]
    public string Username{get; set;}

    [Required(ErrorMessage = "The Contactnumber can not be empty.")]
    [RegularExpression(@"^[0-9]{10}",ErrorMessage = "Contactnumber can only contain numbers and must be have length of 10.")]
    public string Contactnumber { get; set;}

    [Required(ErrorMessage = "Zipcode can not be empty.")]
    [RegularExpression(@"^[0-9]{6}$")]
    public string Zipcode {get; set;}

    [Required(ErrorMessage = "Country can not be Empty.")]
    public string Country{get; set;}

    [Required(ErrorMessage = "State can not be Empty.")]
    public string State {get; set;}

    [Required(ErrorMessage = "City can not be Empty.")]
    public string City {get; set;}

    public string ?Imageurl {get; set;}

    [Required(ErrorMessage = "Address can not be Empty.")]
    public string Address {get; set;}

    public DateTime? Updatedat {get; set;}
    public int? Updatedby{get; set;}

    public IFormFile? ProfileImage{get;set;}

    public static implicit operator ProfileViewModel(User v)
    {
        throw new NotImplementedException();
    }
}