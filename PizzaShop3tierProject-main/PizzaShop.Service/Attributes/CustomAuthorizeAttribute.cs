using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using PizzaShop.Repository.Data;
using PizzaShop.Service.Interfaces;



namespace pizzashop.service.Attributes
{
    public class CustomAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        private readonly string _moduleid;
        private readonly string _permissionType;

        public CustomAuthorizeAttribute(string? moduleid = null, string? permissionType = null)
        {
            _moduleid =moduleid;
            _permissionType = permissionType;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            OnAuthorizationAsync(context).GetAwaiter().GetResult();
        }
        public static JwtSecurityToken ValidateToken(string token)
        {
            if (token == null)
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Convert.ToString("MyNameis_Shiv_Jadav_018_Tatvasoft_007_James_Bond"));
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero

                }, out SecurityToken validatedToken);

                // Corrected access to the validatedToken
                var jwtToken = (JwtSecurityToken)validatedToken;
                
               
                return jwtToken;
            }
            catch
            {
                return null;
            }
        }
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            // var jwtService = context.HttpContext.RequestServices.GetService(typeof(IJwtService)) as IJwtService;
            var permissionService = context.HttpContext.RequestServices.GetService(typeof(IRoleandPermissionServices)) as IRoleandPermissionServices;

            // Get the token from Cookie
            var token = context.HttpContext.Request.Cookies["jwtCookie"];

            // Validate Token
            var principal = ValidateToken(token ?? "");
            if (principal == null)
            {
                // If token is invalid or absent, redirect to the login page
                context.Result = new RedirectToActionResult("Login", "Authenticate", null);
                return;
            }

            // Set user principal (user details)
            // context.HttpContext.User = principal;

            // If no module or permission type is provided, skip permission check
            if (string.IsNullOrEmpty(_moduleid) || string.IsNullOrEmpty(_permissionType))
            {
                return; 
            }

            // Get the user's role from the claims in the token
            var role = principal.Claims.FirstOrDefault(c => c.Type == "Roleid")?.Value;

            if (role == null)
            {
                // If role is missing in the claims, redirect to an error page
                context.Result = new RedirectToActionResult("errorPage", "ErrorPage", new { statusCode = 403 });
                return;
            }

            // Fetch permissions for the user's role and the module
            Permission permission =  permissionService?.GetPermissionsByRoleandModule(Convert.ToInt32(role), Convert.ToInt32(_moduleid));

            // If permission object is null, redirect to error page (permissions not found)
            if (permission == null)
            {
                context.Result = new RedirectToActionResult("errorPage", "ErrorPage", new { statusCode = 403 });
                return;
            }

            bool hasPermission = _permissionType switch
            {
                "CanView" => permission.Canview ,
                "CanAddEdit" => permission.Canadd ,
                "CanDelete" => permission.Candelete ,
                _ => false 
            };

            if (!hasPermission)
            {
                context.Result = new RedirectToActionResult("errorPage", "ErrorPage", new { statusCode = 403 });
            }
        }
    }
}