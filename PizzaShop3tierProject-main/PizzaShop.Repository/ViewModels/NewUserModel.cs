using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace PizzaShop.Repository.ViewModels;

public partial class NewUserModel
{

    public int ?UserId { get; set; }

   [Required(ErrorMessage = "The Firstname can not be empty.")]
    [RegularExpression(@"^[a-zA-Z]+",ErrorMessage = "Firstname can only contain characters.")]
    public string Firstname { get; set; } = null!;

     [Required(ErrorMessage = "The Lastname can not be empty.")]
    [RegularExpression(@"^[a-zA-Z]+",ErrorMessage = "Lastname can only contain characters.")]
    public string Lastname { get; set; } = null!;

   [Required(ErrorMessage = "The Username can not be empty.")]
    [RegularExpression(@"^[a-zA-Z0-9._]+",ErrorMessage = "Username can only contain characters, numbers and . or _ .")]
    public string Username { get; set; } = null!;

     [Required(ErrorMessage = "Email is required")]
     [RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}",ErrorMessage ="Please Enter Proper Email.")]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "Password is required")]
    [RegularExpression(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@@$%^&*-]).{8,}$",ErrorMessage = "The password must contain 1 special character,1 number,1 small character,1 capital character and must be 8 character long.")]
    public string Password { get; set; } = null!;

    [Required(ErrorMessage = "The Contactnumber can not be empty.")]
    [RegularExpression(@"^[0-9]{10}",ErrorMessage = "Contactnumber can only contain numbers and must be have length of 10.")]

    public string Contactnumber { get; set; }
    
 
    public int RoleId { get; set; }

[Required(ErrorMessage = "Country can not be Empty.")]
    public string? Country { get; set; }

    [Required(ErrorMessage = "State can not be Empty.")]
    public string? State { get; set; }

    [Required(ErrorMessage = "City can not be Empty.")]
    public string? City { get; set; }

    [Required(ErrorMessage = "Address can not be Empty.")]
    public string? Address { get; set; }

    [Required(ErrorMessage = "Zipcode can not be empty.")]
    [RegularExpression(@"^[0-9]{6}$")]
    public string Zipcode { get; set; }

   public IFormFile? UserImage{get; set;}
    public string? Imageurl { get; set; }
    public bool? Status { get; set; }
    public int? Createdby { get; set; }

    public int? Updatedby { get; set; }
}