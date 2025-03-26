using PizzaShop.Repository.Data;
using PizzaShop.Repository.ViewModels;

namespace PizzaShop.Repository.Interfaces;

public interface IRoleandPermission {

    public Role GetRolebyId(int id);

    public IQueryable<Permission> GetPermissionsByRole(int roleid);

    public Message UpdatingPermissions(IEnumerable<Permission> permissions);
    public Permission GetPermissionByRoleAndModuleid(int roleid , int moduleid);

}