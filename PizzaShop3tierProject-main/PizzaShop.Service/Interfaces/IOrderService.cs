using PizzaShop.Repository.Data;
using PizzaShop.Repository.ViewModels;

namespace PizzaShop.Service.Interfaces;

public interface IOrderService {


    public List<Order> GetOrdersList();
    public List<OrdersElementView> GetOrdersElementViews(OrdersPaginationView ordersPaginationView);
    public List<Order> GetOrdersForSearch(DateTime from , DateTime to,List<Order> orders);
    public MemoryStream ExportService(string time,string status,int searchString);

}