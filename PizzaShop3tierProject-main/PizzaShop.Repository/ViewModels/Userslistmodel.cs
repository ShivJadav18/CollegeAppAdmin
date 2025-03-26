
using PizzaShop.Repository.Data;

namespace PizzaShop.Repository.ViewModels;

public class Userlistmodel{

    public List<User> Userslist { get; set;}

    public int totalUsers {get;set;}
   
    public int pageno {get; set;}

    public string searchval{get;set;}

    public int count {get;set;}

    }
  