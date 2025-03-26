using System.ComponentModel.DataAnnotations;

namespace PizzaShop.Repository.ViewModels;

public class ModifierGroupViewModel{

    public int? groupid {get;set;}
    public string? Groupname { get; set; }

    
    public string? Description { get; set; }

    public List<int>? modifiersIds{ get ; set;}
    public int? Createdby { get; set; }

    public int? Updatedby { get; set; }

}