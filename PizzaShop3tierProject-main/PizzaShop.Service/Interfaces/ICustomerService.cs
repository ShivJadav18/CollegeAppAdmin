using PizzaShop.Repository.Data;
using PizzaShop.Repository.ViewModels;

namespace PizzaShop.Service.Interfaces;

public interface ICustomerService{
    public List<Customer> GetCustomersService();
    public CustomersPaginationView GetCustomersPaginationView(CustomersPaginationView customersPaginationViewin);
    public MemoryStream ExportCustomersService(CustomersPaginationView customersPaginationViewIn);
}