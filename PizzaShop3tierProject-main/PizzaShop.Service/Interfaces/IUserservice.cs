using PizzaShop.Repository.Data;
using PizzaShop.Repository.ViewModels;

namespace PizzaShop.Service.Interfaces;

public interface IUserservice{

    public ProfileViewModel GetProfileService(string email);

    public ProfileViewModel UpdateProfileService(ProfileViewModel user);

    public Userlistmodel GetUsersListService(Userlistmodel userslist);

    public Message AddUserService(NewUserModel userobj,string email);

    public Usertemp GetUsertemp(int id);

    public bool EditUserService(Usertemp usertemp);

    public void DeleteUserService(int id);
}