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

        public Order GetOrder(int orderid){
        try{
            Order order = _context.Orders.Include(o => o.Customer).FirstOrDefault(o => o.OrderId == orderid);
            if(order == null){
                return new Order{};
            }
            return order;
        }catch(Exception e){
            return new Order{};
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

    public List<Tax> GetEnableTax(){
        try{
            List<Tax> taxes = _context.Taxes.Where(tax => tax.Isdeleted == false && tax.Isenabled == true).ToList();
            if(!taxes.Any()){
                return new List<Tax>{};
            } 
            return taxes;
        }catch(Exception e){
            return new List<Tax>{};
        }
    }

    public List<Item> GetItemsForOrder(int orderid){
        try{
            List<int> ids = _context.Ordertoitems.Where(o => o.OrderId == orderid).Select(o =>(int) o.ItemId).ToList();
            if(!ids.Any()){
                return new List<Item>{};
            }
            List<Item> items = new List<Item>{};
            foreach(int id in ids){
                Item item = _context.Items.FirstOrDefault(i => i.ItemId == id);
                if(item != null){
                    items.Add(item);
                }
            }
            return items;
        }catch(Exception e){
            return new List<Item>{};
        }
    }

    public List<Ordertoitem> GetOrdertoitems(int orderid){
        try{
            List<Ordertoitem> ordertoitems = _context.Ordertoitems.Include(o => o.Item).Where(o => o.OrderId == orderid).ToList();
            return ordertoitems;
        }catch(Exception e){
            return new List<Ordertoitem>{};
        }
    }

    public Item GetItem(int itemid){
        try{
            Item item = _context.Items.FirstOrDefault(i => i.ItemId == itemid);
            if(item == null){
                return new Item{};
            }
            return item;
        }catch(Exception e){
            return new Item{};
        }
    }

     public Ordertotable GetOrdertotablesForOrder(int orderid){
        try{
            Ordertotable ordertotable = _context.Ordertotables.Include(o => o.Table).FirstOrDefault(o => o.OrderId == orderid);
            return ordertotable;
        }catch(Exception e){
            return new Ordertotable{};
        }
    }

    public Table GetTableForOrder(int tableid){
        try{
            Table table = _context.Tables.FirstOrDefault(t => t.TableId == tableid);
            if(table == null){
                return new Table{};
            }
            return table;
        }catch(Exception e){
            return new Table{};
        }
    }

    public Section GetSectionForOrder(int sectionid){
        try{
            Section section = _context.Sections.FirstOrDefault(s => s.SectionId == sectionid);
            if(section == null){
                return new Section{};
            }
            return section;
        }catch(Exception e){
            return new Section{};
        }
    }

    public List<Orderitemmodifier> GetOrderitemmodifiers(int id){
        try{
            List<Orderitemmodifier> orderitemmodifiers = _context.Orderitemmodifiers.Include(o => o.Modifier).Where(o => o.OrdertoitemId == id).ToList();
            return orderitemmodifiers;
        }catch(Exception e){
            return new List<Orderitemmodifier>{};
        }
    }


}