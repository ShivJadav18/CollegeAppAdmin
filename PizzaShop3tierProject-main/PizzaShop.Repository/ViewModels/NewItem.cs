using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
namespace PizzaShop.Repository.ViewModels;

public class NewItem{

    public int ? itemid{get; set;}

    [Required(ErrorMessage = "Item Name can not be empty.")]
    [RegularExpression("/^[a-zA-Z]+$/")]
    public string Name { get; set; } = null!;

    [Required(ErrorMessage = "Item Description can not be empty.")]
    [RegularExpression("/^[a-zA-Z]+$/")]
    public string? Description { get; set; }

    [Required(ErrorMessage = "Item Type can not be empty.")]
    public string? Itemtype { get; set; }

    [Required(ErrorMessage = "Item Rate can not be empty.")]
   
    public decimal? Rate { get; set; }  

    [Required(ErrorMessage = "Item Quantity can not be empty.")]
    public int? Quantity { get; set; }

    public bool Isavailable { get; set; }

    [Required(ErrorMessage = "Item Category can not be empty.")]
    public int? CategoryId { get; set; }

    [Required(ErrorMessage = "Item Unit can not be empty.")]
    public int? UnitId { get; set; }

    public bool Defaulttax { get; set; }

    [RegularExpression("/^[0-9]+$/")]
    public decimal? Taxpercentage { get; set; }

    public string? Shortcode { get; set; }

    public string? Imageurl { get; set; }

    public int? Createdby { get; set; }

    public int? Updatedby { get; set; }

    public List<ModifierGroupAndMinMax> Groups{get; set;}
    public DateTime? Updatedat{ get; set;}
    public IFormFile ItemImage{ get ; set;}
}