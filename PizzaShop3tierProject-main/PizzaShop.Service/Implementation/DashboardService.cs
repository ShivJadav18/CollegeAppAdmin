using PizzaShop.Repository.Interfaces;
using PizzaShop.Service.Interfaces;
using PizzaShop.Repository.ViewModels;
using PizzaShop.Repository.Data;

namespace PizzaShop.Service.Implementation;

public class DashboardService:IDashbordService{
    private readonly IOrder _order;
    private readonly IMenu _products;
    public DashboardService(IOrder order,IMenu products){
        _products = products;
        _order = order;
    }

    public ValueData GetValueData(){
        
        ValueData valueData = new ValueData{};
        List<Order> orders = _order.GetOrders();
        valueData.TotalOrders = orders.Count;
        valueData.TotalSales = orders.Sum(o => (decimal)o.Totalamount);
        valueData.AvgOrder = orders.Average(o => (decimal)o.Totalamount);
        valueData.AvgOrder = valueData.AvgOrder;
        valueData.TotalProfit = 0;
        foreach(Order order in orders){
            valueData.TotalProfit = valueData.TotalProfit + (decimal)(order.Totalamount/10);
        }
        return valueData;
    }

    public BarChartData GetBarChartData(){
        ValueData valueData = GetValueData();
        BarChartData barChartData = new BarChartData{};

        List<Order> orders = _order.GetOrders();

        var ordersPerMonth = orders
                            .GroupBy(o => new { Month = o.Orderdate.Value.Month, Year = o.Orderdate.Value.Year})
                            .Select(g => new {
                                Year = g.Key.Year,
                                Month = g.Key.Month,
                                Revenue = g.Sum(o => o.Totalamount),
                                Percentage = (g.Sum(o =>(decimal)o.Totalamount) * 100) / valueData.TotalSales
                            })
                            .OrderBy(r => r.Year)
                            .ThenBy(m => m.Month)
                            .ToList();
        foreach(var order in ordersPerMonth){

            switch(order.Month){
                case 1:
                    barChartData.janPer = (int)order.Percentage;
                    break;
                case 2:
                    barChartData.febPer = (int)order.Percentage;
                    break;
                case 3:
                    barChartData.marPer = (int)order.Percentage;
                    break;
                case 4:
                    barChartData.aprPer = (int)order.Percentage;
                    break;
                case 5:
                    barChartData.mayPer = (int)order.Percentage;
                    break;
                case 6:
                    barChartData.junPer = (int)order.Percentage;
                    break;
                case 7:
                    barChartData.julPer = (int)order.Percentage;
                    break;
                case 8:
                    barChartData.augPer = (int)order.Percentage;
                    break;
                case 9:
                    barChartData.sepPer = (int)order.Percentage;
                    break;
                case 10:
                    barChartData.octPer = (int)order.Percentage;
                    break;
                case 11:
                    barChartData.novPer = (int)order.Percentage;
                    break;
                case 12:
                    barChartData.decPer = (int)order.Percentage;
                    break;
            }
        }
        return barChartData;
    }

    public List<ForPie> GetListOfPieValues(){
        List<ForPie> pieValues = new List<ForPie>{};
        List<Order> orders = _order.GetOrders();
        List<Category> categories = _products.GetCategories();
        decimal totalRevenue = orders.Sum(o => (decimal)o.Totalamount);
        Console.WriteLine("hey"+totalRevenue);
        var listCompanyvise = _order.GetAllOrdertoItem()
                            .GroupBy(o => new{category = o.Item.CategoryId})
                            .Select(t => new{
                                Percentage = (t.Sum(t => t.Amount) * 100) / totalRevenue,
                                Category = t.Key.category 
                            })
                            .ToList();

        foreach(var temp in listCompanyvise){
            string category = categories.FirstOrDefault(c => c.CategoryId == temp.Category).Categoryname;
            ForPie pie = new ForPie{};
            pie.companyName = category;
            pie.Percentage = (decimal)temp.Percentage;
            pieValues.Add(pie);
        }
        
        return pieValues;
    }


}