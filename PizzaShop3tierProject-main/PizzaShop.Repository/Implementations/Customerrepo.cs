using Microsoft.EntityFrameworkCore;
using PizzaShop.Repository.Data;
using PizzaShop.Repository.Interfaces;

namespace PizzaShop.Repository.Implementations;

public class Customerrepo : ICustomer{
    private readonly ElectronicDataBaseContext _context;

    public Customerrepo(ElectronicDataBaseContext context){
        _context = context;
    }

    public List<Customer> GetAllCustomers(){
        try{
            List<Customer> customers = _context.Customers.Include(c => c.Orders).ToList();
            return customers;
        }catch(Exception e){
            return new List<Customer>{};
        }
    }

    
}