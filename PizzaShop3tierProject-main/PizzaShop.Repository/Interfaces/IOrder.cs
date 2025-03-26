using PizzaShop.Repository.Data;

namespace PizzaShop.Repository.Interfaces;

public interface IOrder{
    public List<Order> GetOrders();
    public Rating GetRatingsByOrderId(int order_id);
    public Customer GetCustomerByCustomerId(int customer_id);
    public Payment GetPaymentByOrderId(int order_id);
}