using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace PizzaShop.Repository.ViewModels;

public class NewTable{

    public string name{get;set;}
    public int? tableid{get;set;}
    public int sectionid {get;set;}
    public bool status{get;set;}
    public int Capacity{get;set;}
    public int Createdby{get;set;}
    public int Updatedby{get;set;}
    public DateTime? Updatedat{get;set;}

}