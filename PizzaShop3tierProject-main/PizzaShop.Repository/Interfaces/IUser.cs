using PizzaShop.Repository.Data;
using PizzaShop.Repository.ViewModels;
 
 namespace PizzaShop.Repository.Interfaces;

 public interface IUser{

    public User GetUser(Userlogin user);

    public User SetPass(Userlogin user,string Currentpass);

    public User UpdateUser(ProfileViewModel user);

    public IQueryable<User> GetUserslist(string searchval);

    public Message AddUser(User userobj);

    public User GetUserById(int id);

     public bool EditUser(Usertemp user);

     public void DeleteUser(int id);

     public Message IsRepeatedUsername(string username,string email);

     public Message SetTokenForResetPass(int userid,string token,DateTime ExpireTime);

     public Resettoken ValidateTokenForResetPass(string token);

     public Message UpdateResetToken(Resettoken resettoken);
}