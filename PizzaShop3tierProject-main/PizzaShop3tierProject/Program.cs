using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PizzaShop.Repository.Data;
using PizzaShop.Repository.Implementations;
using PizzaShop.Repository.Interfaces;
using PizzaShop.Service.Implementation;
using PizzaShop.Service.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDistributedMemoryCache();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
  {
      options.SaveToken = true;
      options.RequireHttpsMetadata = false;
      options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
      {
          ValidateIssuer = true,
          ValidateAudience = true,
          ValidAudience = "dotnetclient",
          ValidIssuer = "Pizzashop App",
          ClockSkew = TimeSpan.Zero,// It forces tokens to expire exactly at token expiration time instead of 5 minutes later
          IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("MyNameis_Shiv_Jadav_018_Tatvasoft_007_James_Bond"))
      };
  });

builder.Services.AddSession(options =>
{
    options.Cookie.IsEssential = true;
});

builder.Services.AddDbContext<PizzaShopDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddScoped<IAuthenticate,Authenticate>();
builder.Services.AddScoped<IUser,Userop>();
builder.Services.AddScoped<IUserservice,Userservice>();
builder.Services.AddScoped<IRoleandPermission,RoleandPermission>();
builder.Services.AddScoped<IRoleandPermissionServices,RoleandPermissionServices>();
builder.Services.AddScoped<IMenu,Menu>();
builder.Services.AddScoped<IMenuService,MenuService>();
builder.Services.AddScoped<ITableandSection,TableandSection>();
builder.Services.AddScoped<ITableandSectionService,TableandSectionService>();
builder.Services.AddScoped<ITaxFees,TaxFees>();
builder.Services.AddScoped<ITaxServices,TaxServices>();
builder.Services.AddScoped<IOrder,Orderr>();
builder.Services.AddScoped<IOrderService,OrderService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseMiddleware<JwtMiddleware>();

//For enabling Authentication and Authorization.
app.UseAuthentication();
app.UseAuthorization();

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Authenticate}/{action=Login}/{id?}");

app.Run();
