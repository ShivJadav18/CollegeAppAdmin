using System.Reflection.Metadata;
using Microsoft.EntityFrameworkCore;
using PizzaShop.Repository.Data;
using PizzaShop.Repository.Interfaces;
using PizzaShop.Repository.ViewModels;

namespace PizzaShop.Repository.Implementations;

public class Userop : IUser{
    private readonly PizzaShopDbContext _context;
    public Userop(PizzaShopDbContext context){
        _context = context;
    }
 
    public User GetUser(Userlogin user){
        User userobj = _context.Users.Include(u => u.Role).FirstOrDefault(u => u.Email == user.Email && u.Isdeleted == false && u.Status == true );

        if(userobj == null){
            return new User{};
        }

        return userobj;
    }

    public User GetUserById(int id){
        User userobj = _context.Users.FirstOrDefault(u => u.UserId == id);

        return userobj;
    }

    public bool EditUser(Usertemp user){
        try{
        User userreal = _context.Users.FirstOrDefault(u => u.UserId == user.UserId);

                userreal.Email = user.Email;
                userreal.Firstname = user.Firstname;
                userreal.Lastname = user.Lastname;
                userreal.Username = user.Username;
                userreal.Country = user.Country;
                userreal.State = user.State;
                userreal.City = user.City;
                userreal.Status = user.Status;
                userreal.Address = user.Address;
                userreal.Contactnumber = user.Contactnumber;
                userreal.Zipcode = user.Zipcode;
                userreal.RoleId = user.RoleId;
                userreal.Imageurl = user.Imageurl;
                userreal.Updatedat = user.Updatedat;
                userreal.Updatedby = user.Updatedby;

                _context.SaveChanges();
                return true;
        }catch(Exception e){
            return false;
        }
    }

    public void DeleteUser(int id){
      User user = GetUserById(id);

      user.Isdeleted = true;

      _context.SaveChanges();
    }

    public User SetPass(Userlogin user,string Currentpass){
         User userobj = _context.Users.Include(u => u.Role).FirstOrDefault(u => u.Email == user.Email);

         if(userobj == null){
            return new User{};
        }
        
        userobj.Password = user.Password;

        _context.SaveChanges();

        return userobj;
    }

    public Message IsRepeatedUsername(string username,string email){

        var repeatedUser = _context.Users.FirstOrDefault(u => u.Username == username && u.Email != email);

        if(repeatedUser != null){
            return new Message{error = true , errorMessage = "You can not use this Username."};
        }
        return new Message{error = false};
    }
    public User UpdateUser(ProfileViewModel usertemp){
        try{
        var user = _context.Users.Include(u => u.Role).FirstOrDefault(u => u.Email == usertemp.Email);
        

        user.Firstname = usertemp.Firstname;
        user.Lastname = usertemp.Lastname;
        user.Address = usertemp.Address;
        user.Zipcode = usertemp.Zipcode;
        user.Username = usertemp.Username;
        user.Country = usertemp.Country;
        user.City = usertemp.City;
        user.State = usertemp.State;
        user.Contactnumber = usertemp.Contactnumber;
        user.Updatedby = usertemp.Updatedby;
        user.Updatedat = usertemp.Updatedat;
        user.Imageurl = usertemp.Imageurl;
        

        _context.SaveChanges();

        return user;
        }catch(Exception e){
            return new User{};
        }

    }

    public IQueryable<User> GetUserslist(string searchval){
        return  _context.Users.Include(u => u.Role).Where(u => (string.IsNullOrEmpty(searchval) || u.Username.Contains(searchval)) && u.Isdeleted == false);
    }

    public Message AddUser(User userobj){
        try{
        _context.Add(userobj);
        _context.SaveChanges();
        return new Message{error = false};
        }catch(Exception e){
            return new Message {error = true , errorMessage = e.Message};
        }
    }

    public Message SetTokenForResetPass(int userid,string token,DateTime ExpireTime){
        try{
            Resettoken resettoken = new Resettoken{UserId = userid,Token = token, Expiredat = ExpireTime};
            _context.Add(resettoken);
            _context.SaveChanges();
            return new Message{error = false};
        }catch(Exception e) {
            return new Message{error = true, errorMessage = e.Message};
        }
    }

    public Resettoken ValidateTokenForResetPass(string token){
        try{
            Resettoken resettoken = _context.Resettokens.FirstOrDefault(r => r.Token == token);
            if(resettoken == null){
                return new Resettoken{};
            }
            return resettoken;
        }catch(Exception e){
            return new Resettoken{};
        }
    }

    public Message UpdateResetToken(Resettoken resettoken){
        try{
        resettoken.Isreseted = true;
        _context.SaveChanges();
        return new Message{error = false};
        }catch(Exception e){
            return new Message{error = true, errorMessage = e.Message};
        }
    }
}