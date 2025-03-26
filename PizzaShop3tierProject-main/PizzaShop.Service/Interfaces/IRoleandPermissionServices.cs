using PizzaShop.Repository.Data;
using PizzaShop.Repository.ViewModels;

namespace PizzaShop.Service.Interfaces;

public interface IRoleandPermissionServices{

    public Role GetRoleByIdService(int id);

    public IQueryable<Permission> GetPermissionsByRoleService(int roleid);

    public Message UpdatePermissionsService(IEnumerable<Permission> permissions,int id);
    public Permission GetPermissionsByRoleandModule(int roleid, int moduleid);
}