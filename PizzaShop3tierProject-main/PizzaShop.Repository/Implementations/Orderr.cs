using Microsoft.EntityFrameworkCore;
using PizzaShop.Repository.Data;
using PizzaShop.Repository.Interfaces;

namespace PizzaShop.Repository.Implementations;

public class Orderr : IOrder{
    private readonly PizzaShopDbContext _context;

    public Orderr(PizzaShopDbContext context){
        _context = context;
    }

    public List<Order> GetOrders(){
        try{
            List<Order> orders = _context.Orders.Include(o => o.Customer).ToList();
            return orders;
        }catch(Exception e){
            return new List<Order>{};
        }
    }

    public Rating GetRatingsByOrderId(int order_id){
        try{
            Rating ratings = _context.Ratings.FirstOrDefault(r => r.OrderId == order_id);
            if(ratings == null){
                return new Rating{};
            }
            return ratings;
        }catch(Exception e){
            return new Rating{};
        }
    }

    public Customer GetCustomerByCustomerId(int customer_id){
        try{
            Customer customer = _context.Customers.FirstOrDefault(c => c.CustomerId == customer_id && c.Isdeleted == false);
            if(customer == null){
                return new Customer{};
            }
            return customer;
        }catch(Exception e){
            return new Customer{};
        }
    }

    public Payment GetPaymentByOrderId(int order_id){
        try{
            Payment payment = _context.Payments.FirstOrDefault(p => p.OrderId == order_id);
            if(payment == null){
                return new Payment{};
            }
            return payment;
        }catch(Exception e){
            return new Payment{};
        }
    }

}