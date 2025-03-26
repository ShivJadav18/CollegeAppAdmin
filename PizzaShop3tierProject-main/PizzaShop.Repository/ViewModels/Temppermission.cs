namespace PizzaShop.Repository.ViewModels;

public class Temppermission{

    public int PermissionfieldId { get; set; }

    public int RoleId { get; set; }

    public bool Canview { get; set; }

    public bool Canadd { get; set; }

    public bool Candelete { get; set; }

     public int? Createdby { get; set; }

    public DateTime? Createdat { get; set; }

}