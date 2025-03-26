using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MimeKit;
using MailKit.Net.Smtp;
using PizzaShop.Repository.Data;
using PizzaShop.Repository.Interfaces;
using PizzaShop.Repository.ViewModels;
using PizzaShop.Service.Interfaces;

namespace PizzaShop.Service.Implementation;

public class Authenticate : IAuthenticate
{

    private readonly IUser _repouser;

    public Authenticate(IUser repouser)
    {
        _repouser = repouser;
    }

    public Message ResetPassValidationService(string email)
    {
        Userlogin usertempobj = new Userlogin { Email = email };
        User user = _repouser.GetUser(usertempobj);
        if(user.Email == null){
            return new Message { error = true , errorMessage = "User's status is in-active or user is deleted or This user is not exist."};
        }
        string token = Guid.NewGuid().ToString();
        DateTime ExpireTime = DateTime.Now.AddHours(24);
        Message message = _repouser.SetTokenForResetPass(user.UserId, token, ExpireTime);

        if (message.error)
        {
            return message;
        }
        message.errorMessage = token;
        return message;
    }

    public Message TokenValidationForResetPass(string token)
    {

        Resettoken resettoken = _repouser.ValidateTokenForResetPass(token);

        if (string.IsNullOrEmpty(resettoken.Token))
        {
            return new Message { error = true, errorMessage = "You are not authorized." };
        }

        if (resettoken.Isreseted == true)
        {
            return new Message { error = true, errorMessage = "You have used this link before." };
        }

        if (resettoken.Expiredat <= DateTime.Now)
        {
            return new Message { error = true, errorMessage = "This link is Expired." };
        }

        return new Message { error = false };
    }

    public string GetAuthenticate(Userlogin user)
    {
        User userobj = _repouser.GetUser(user);

        if (userobj.Email == null || !BCrypt.Net.BCrypt.Verify(user.Password, userobj.Password))
        {
            return "";
        }

        var role = userobj.Role.Name;
        var claims = new[] {
                new Claim(ClaimTypes.Role , role),
                new Claim("Roleid",userobj.Role.RoleId.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim("UserName",userobj.Username),
                new Claim("Userid",userobj.UserId.ToString())
            };

        var token = GenerateJSONWebToken(claims);

        return token;
    }

    public bool SendEmailForResetpass(string email, string baseUrl, string token = "")
    {
        Userlogin usertempobj = new Userlogin { Email = email };

        User user = _repouser.GetUser(usertempobj);

        if (user.Email == null)
        {
            return false;
        }

        EmailSendingHelper(email, baseUrl, token);
        return true;
    }

    public bool ResetPassService(string email, string password, bool isForChangePassstring = false, string Currentpass = "")
    {

        Userlogin usertempobj = new Userlogin { Email = email, Password = BCrypt.Net.BCrypt.HashPassword(password) };
        User user = _repouser.GetUser(usertempobj);

        if (isForChangePassstring)
        {

            if (!BCrypt.Net.BCrypt.Verify(Currentpass, user.Password))
            {
                return false;
            }

        }

        User userobj = _repouser.SetPass(usertempobj, Currentpass);

        if (userobj.Email == null)
        {
            return false;
        }

        return true;
    }
    public Message SendEmailTONewUserService(string email,string username,string baseUrl,string password){
        try{
            EmailSendingtoNewUser(email,username,baseUrl,password);
            return new Message{error = false};
        }catch(Exception e){
            return new Message{error = true, errorMessage = e.Message};
        }
    }
    private async void EmailSendingtoNewUser(string email,string username,string baseUrl,string password){
         // making of mail
        var message = new MimeKit.MimeMessage();
        message.From.Add(new MailboxAddress("Pizza Shop", "test.dotnet@etatvasoft.com"));
        message.To.Add(new MailboxAddress("PizzaShop User", email));
        message.Subject = "Reset your password ";

        // making of url link
         string emailTemplate = System.IO.File.ReadAllText("wwwroot/templates/NewUser.html");

        
        emailTemplate = emailTemplate.Replace("[USERNAME]", username);
        emailTemplate = emailTemplate.Replace("[PASSWORD]", password);
        var bodyBuilder = new BodyBuilder();
        var url = $"{baseUrl}";
       
         bodyBuilder.HtmlBody = emailTemplate;


        message.Body = bodyBuilder.ToMessageBody();

        //sending of mail 
        using (var client = new SmtpClient())
        {
            await client.ConnectAsync("mail.etatvasoft.com", 587, false);
            await client.AuthenticateAsync("test.dotnet@etatvasoft.com", "P}N^{z-]7Ilp");
            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }

        return;
    }

    private async void EmailSendingHelper(string email, string baseUrl,string token = "")
    {
        // making of mail
        var message = new MimeKit.MimeMessage();
        message.From.Add(new MailboxAddress("Pizza Shop", "test.dotnet@etatvasoft.com"));
        message.To.Add(new MailboxAddress("PizzaShop User", email));
        message.Subject = "Reset your password ";

        var bodyBuilder = new BodyBuilder();
        var url = $"{baseUrl}";
       
        bodyBuilder.HtmlBody = $"Reset your password <a href='{url}/Authenticate/Resetpage/?token={token}'>Here </a>";
        message.Body = bodyBuilder.ToMessageBody();

        //sending of mail 
        using (var client = new SmtpClient())
        {
            await client.ConnectAsync("mail.etatvasoft.com", 587, false);
            await client.AuthenticateAsync("test.dotnet@etatvasoft.com", "P}N^{z-]7Ilp");
            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }

        return;
    }
    private string GenerateJSONWebToken(Claim[] claims)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("MyNameis_Shiv_Jadav_018_Tatvasoft_007_James_Bond"));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: "Pizzashop App",
            audience: "dotnetclient",
            expires: DateTime.Now.AddDays(1),
            signingCredentials: credentials,
            claims: claims
            );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public string GetUserFromResetToken(string token)
    {
        Resettoken resettoken = _repouser.ValidateTokenForResetPass(token);

        Message message = _repouser.UpdateResetToken(resettoken);

        if (message.error)
        {
            return "";
        }

        int userid = Convert.ToInt32(resettoken.UserId);

        User user = _repouser.GetUserById(userid);

        return user.Email;
    }

}