using Microsoft.EntityFrameworkCore;
using PizzaShop.Repository.Data;
using PizzaShop.Repository.Interfaces;
using PizzaShop.Repository.ViewModels;

namespace PizzaShop.Repository.Implementations;

public class RoleandPermission : IRoleandPermission{

    private readonly PizzaShopDbContext _context;

    public RoleandPermission(PizzaShopDbContext context){
        _context = context;
    }

    public Role GetRolebyId(int id){
        try{
            Role role = _context.Roles.FirstOrDefault(r => r.RoleId == id);

            if(role == null){
            return new Role{};
            }

            return role;
        }catch(Exception e){
            return new Role{};
        }
    }

    public IQueryable<Permission> GetPermissionsByRole(int roleid){
        var permissions = _context.Permissions.Include(p => p.Permissionfield).Where(p => p.RoleId == roleid);
        return permissions;
    }

    public Message UpdatingPermissions(IEnumerable<Permission> permissions){
        try{
        foreach(var permission in permissions){
            Permission permissionexist = _context.Permissions.FirstOrDefault(p => p.PermissionId == permission.PermissionId);
            if(permissionexist == null){
                return new Message{error = true , errorMessage = "Some Internal Error."};
            }  
            permissionexist.Canadd = permission.Canadd;
            permissionexist.Candelete = permission.Candelete;
            permissionexist.Canview = permission.Canview;
            permissionexist.Updatedby = permission.Updatedby;
            permissionexist.Updatedat = permission.Updatedat;
        }
        _context.SaveChanges();
        return new Message{error = false};
        }catch(Exception e){
            return new Message{error = true , errorMessage = e.Message};
        }
    }

    public Permission GetPermissionByRoleAndModuleid(int roleid , int moduleid){
        try{
            Permission permission = _context.Permissions.FirstOrDefault(p => p.RoleId == roleid && p.PermissionfieldId == moduleid);
            return permission;
        }catch(Exception e){
            return new Permission{};
        }
    }
}