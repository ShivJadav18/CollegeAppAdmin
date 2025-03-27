using PizzaShop.Repository.Data;

namespace PizzaShop.Repository.Interfaces;

public interface IOrder{
    public List<Order> GetOrders();
    public Rating GetRatingsByOrderId(int order_id);
    public Customer GetCustomerByCustomerId(int customer_id);
    public Payment GetPaymentByOrderId(int order_id);
    public Order GetOrder(int orderid);
    public List<Tax> GetEnableTax();
    public List<Item> GetItemsForOrder(int orderid);
    public List<Ordertoitem> GetOrdertoitems(int orderid);
    public Item GetItem(int itemid);
    public Ordertotable GetOrdertotablesForOrder(int orderid);
    public Table GetTableForOrder(int tableid);
    public Section GetSectionForOrder(int sectionid);
    public List<Orderitemmodifier> GetOrderitemmodifiers(int id);
}