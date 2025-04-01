using PizzaShop.Repository.Data;

namespace PizzaShop.Repository.Interfaces;

public interface ICustomer{
    public List<Customer> GetAllCustomers();
}