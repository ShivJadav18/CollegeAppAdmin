
using Org.BouncyCastle.Asn1.Ocsp;
using PizzaShop.Repository.Data;
using PizzaShop.Repository.Interfaces;
using PizzaShop.Repository.ViewModels;
using PizzaShop.Service.Interfaces;

namespace PizzaShop.Service.Implementation;


public class Userservice : IUserservice{

    private readonly IUser _repouser;

    public Userservice (IUser repouser){
        _repouser = repouser;
    }

    public ProfileViewModel GetProfileService(string email){
        var usertemp = new Userlogin{Email = email};
        User userobj = _repouser.GetUser(usertemp);

        ProfileViewModel user = new ProfileViewModel{
            Email =userobj.Email,
            Rolename = userobj.Role.Name,
            Firstname = userobj.Firstname,
            Lastname = userobj.Lastname,
            Username = userobj.Username,
            Address = userobj.Address,
            City = userobj.City,
            State = userobj.State,
            Country = userobj.Country,
            Imageurl = userobj.Imageurl,
            Zipcode = userobj.Zipcode,
            Contactnumber = userobj.Contactnumber
        };

        if(userobj.Email == null){
        return new ProfileViewModel{};
        }

        return user;
    }

    public Usertemp GetUsertemp(int id){
        User userobj = _repouser.GetUserById(id);

        Usertemp usertemp = new Usertemp{
                Email = userobj.Email,
                UserId = userobj.UserId,
                Firstname = userobj.Firstname,
                Lastname = userobj.Lastname,
                Username = userobj.Username,
                Country = userobj.Country,
                State = userobj.State,
                City = userobj.City,
                Status = userobj.Status,
                Address = userobj.Address,
                Contactnumber = userobj.Contactnumber,
                Zipcode = userobj.Zipcode,
                RoleId = userobj.RoleId,
                Imageurl = userobj.Imageurl
        };

        return usertemp;
    }

    public bool EditUserService(Usertemp usertemp){
         Message message = _repouser.IsRepeatedUsername(usertemp.Username,usertemp.Email);

        if(message.error){
            return false;
        }
        if(usertemp.UserImage != null){
            
            var fileName = Path.GetFileNameWithoutExtension(usertemp.UserImage.FileName);
                var extension = Path.GetExtension(usertemp.UserImage.FileName);
                var uniqueFileName = $"{fileName}_{Guid.NewGuid()}{extension}";

                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/UploadedImages");
                var path = Path.Combine(uploadsFolder, uniqueFileName);
                
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    usertemp.UserImage.CopyTo(fileStream);
                }

                // Save the relative path to the usertemp property
                usertemp.Imageurl = $"UploadedImages/{uniqueFileName}";
        }
        usertemp.Updatedat = DateTime.Now;
        bool result =_repouser.EditUser(usertemp);
        if(result){
            return true;
        }

        return false;
    }

    public Userlistmodel GetUsersListService( Userlistmodel userslistobj){
        var totaluserslist = _repouser.GetUserslist(userslistobj.searchval);
        var totalUsers = totaluserslist.Count();
        var userslist = totaluserslist.Skip((userslistobj.pageno-1)* userslistobj.count).Take(userslistobj.count).ToList();

        var Userlistmodel = new Userlistmodel{
                totalUsers =totalUsers,
                Userslist = userslist,                              
                count = userslistobj.count,
                pageno = userslistobj.pageno
            };

        return Userlistmodel;
    }

    public void DeleteUserService(int id){
        _repouser.DeleteUser(id);
    }

    public Message AddUserService(NewUserModel userobj,string email){
        Message message = _repouser.IsRepeatedUsername(userobj.Username,email);

        if(message.error){
            message.errorMessage = "This Username or Email is repeated.";
            return message;
        }

        Userlogin usetemp = new Userlogin{Email = email};
        User addinguser = _repouser.GetUser(usetemp);
        userobj.Password = BCrypt.Net.BCrypt.HashPassword(userobj.Password);
        userobj.Createdby = addinguser.UserId;
        userobj.Updatedby = addinguser.UserId;
        if(userobj.UserImage != null){
            
            var fileName = Path.GetFileNameWithoutExtension(userobj.UserImage.FileName);
                var extension = Path.GetExtension(userobj.UserImage.FileName);
                var uniqueFileName = $"{fileName}_{Guid.NewGuid()}{extension}";

                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/UploadedImages");
                var path = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    userobj.UserImage.CopyTo(fileStream);
                }

                // Save the relative path to the usertemp property
                userobj.Imageurl = $"UploadedImages/{uniqueFileName}";
        }
        User newuser = new User{
            Firstname = userobj.Firstname,
            Lastname = userobj.Lastname,
            Username = userobj.Username,
            Email = userobj.Email,
            Contactnumber = userobj.Contactnumber,
            Address = userobj.Address,
            Country = userobj.Country,
            State = userobj.State,
            City = userobj.City,
            Zipcode = userobj.Zipcode,
            Imageurl = userobj.Imageurl,
            RoleId = userobj.RoleId,
            Createdby = userobj.Createdby,
            Updatedby = userobj.Updatedby,
            Password = userobj.Password
        };

        Message result =_repouser.AddUser(newuser);

        if(result.error){
            return result;
        }
        return new Message{error = false};
    }

    public ProfileViewModel UpdateProfileService(ProfileViewModel usertemp){

        Message message = _repouser.IsRepeatedUsername(usertemp.Username, usertemp.Email);

        if(message.error){
            usertemp.Username = "repeated";
            return usertemp;
        }

        usertemp.Updatedat = DateTime.Now;

        if(usertemp.ProfileImage != null){
            
            var fileName = Path.GetFileNameWithoutExtension(usertemp.ProfileImage.FileName);
                var extension = Path.GetExtension(usertemp.ProfileImage.FileName);
                var uniqueFileName = $"{fileName}_{Guid.NewGuid()}{extension}";

                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/UploadedImages");
                var path = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    usertemp.ProfileImage.CopyTo(fileStream);
                }

                // Save the relative path to the usertemp property
                usertemp.Imageurl = $"UploadedImages/{uniqueFileName}";
        }

        User updateduser = _repouser.UpdateUser(usertemp);
        
        if(updateduser.Email == null){
        return new ProfileViewModel{};
        }
    
        usertemp.Rolename = updateduser.Role.Name;

        return usertemp;
    }
}