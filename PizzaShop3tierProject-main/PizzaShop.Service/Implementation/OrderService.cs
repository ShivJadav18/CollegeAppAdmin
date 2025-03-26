
using PizzaShop.Repository.Data;
using PizzaShop.Repository.Interfaces;
using PizzaShop.Repository.ViewModels;
using PizzaShop.Service.Interfaces;
using ClosedXML.Excel;

namespace PizzaShop.Service.Implementation;

public class OrderService : IOrderService
{

    private readonly IOrder _order;

    public OrderService(IOrder order)
    {
        _order = order;
    }
    public List<Order> GetOrdersList()
    {
        return new List<Order> { };
    }

    public List<OrdersElementView> GetOrdersElementViews(OrdersPaginationView ordersPaginationView)
    {

        try
        {
            List<Order> orders = _order.GetOrders();
            if (ordersPaginationView.searchval != 0)
            {
                orders = orders.Where(o => o.OrderId == ordersPaginationView.searchval).ToList();
            }
            if (ordersPaginationView.status != "All Status")
            {
                orders = orders.Where(o => o.Orderstatus == ordersPaginationView.status).ToList();
            }
            if (ordersPaginationView.timefil != "All Time")
            {
                orders = GetOrdersByTime(ordersPaginationView.timefil, orders);
            }
            if (ordersPaginationView.from != null && ordersPaginationView.todate != null)
            {
                orders = GetOrdersForSearch((DateTime)ordersPaginationView.from, (DateTime)ordersPaginationView.todate, orders);
            }
            List<OrdersElementView> ordersElementViews = GetOrdersElementViewsFromOrders(orders);
            if (ordersPaginationView.order != 0 && ordersPaginationView.sortmethod != -1)
            {
                int order = ordersPaginationView.order;
                int method = ordersPaginationView.sortmethod;
                if (order == 1)
                {
                    if (method == 0)
                    {
                        ordersElementViews = ordersElementViews.OrderBy(o => o.order_id).ToList();
                    }
                    else
                    {
                        ordersElementViews = ordersElementViews.OrderByDescending(o => o.order_id).ToList();
                    }
                }
                else if (order == 2)
                {
                    if (method == 0)
                    {
                        ordersElementViews = ordersElementViews.OrderBy(o => o.date).ToList();
                    }
                    else
                    {
                        ordersElementViews = ordersElementViews.OrderByDescending(o => o.date).ToList();
                    }
                }
                else if (order == 3)
                {
                    if (method == 0)
                    {
                        ordersElementViews = ordersElementViews.OrderBy(o => o.customer).ToList();
                    }
                    else
                    {
                        ordersElementViews = ordersElementViews.OrderByDescending(o => o.customer).ToList();
                    }
                }
                else
                {
                    if (method == 0)
                    {
                        ordersElementViews = ordersElementViews.OrderBy(o => o.total_amount).ToList();
                    }
                    else
                    {
                        ordersElementViews = ordersElementViews.OrderByDescending(o => o.total_amount).ToList();
                    }
                }
            }
            return ordersElementViews;
        }
        catch (Exception e)
        {
            return new List<OrdersElementView> { };
        }

    }

    public List<Order> GetOrdersByTime(string time_filter, List<Order> orders)
    {
        DateTime time = new DateTime();
        if (time_filter == "Last 7 days")
        {
            time = DateTime.UtcNow.AddDays(-7);
            orders = orders.Where(o => o.Orderdate >= time).ToList();
        }
        else if (time_filter == "Last 30 days")
        {
            time = DateTime.UtcNow.AddDays(-30);
            orders = orders.Where(o => o.Orderdate >= time).ToList();
        }
        else
        {
            var startOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            var startOfNextMonth = startOfMonth.AddMonths(1);
            orders = orders.Where(o => o.Orderdate >= startOfMonth && o.Orderdate < startOfNextMonth).ToList();
        }

        return orders;
    }

    public List<Order> GetOrdersForSearch(DateTime from, DateTime to, List<Order> orders)
    {
        // List<Order> orders = _order.GetOrders();
        orders = orders.Where(o => o.Orderdate >= from && o.Orderdate <= to).ToList();
        // List<OrdersElementView> ordersElementViews = GetOrdersElementViewsFromOrders(orders);
        return orders;
    }

    private List<OrdersElementView> GetOrdersElementViewsFromOrders(List<Order> orders)
    {
        List<OrdersElementView> ordersElementViews = new List<OrdersElementView> { };
        foreach (Order order in orders)
        {
            OrdersElementView ordersElementView = new OrdersElementView { };
            Customer customer = _order.GetCustomerByCustomerId((int)order.CustomerId);
            ordersElementView.customer = customer.Firstname;
            Rating rating = _order.GetRatingsByOrderId(order.OrderId);
            ordersElementView.rating = (int)rating.Servicerate;
            Payment payment = _order.GetPaymentByOrderId(order.OrderId);
            ordersElementView.payment_mode = payment.Paymentmethod;
            ordersElementView.date = (DateTime)order.Orderdate;
            ordersElementView.order_id = order.OrderId;
            ordersElementView.status = order.Orderstatus;
            ordersElementView.total_amount = (decimal)order.Totalamount;
            ordersElementViews.Add(ordersElementView);
        }
        return ordersElementViews;
    }

    public MemoryStream ExportService(string time, string status, int searchString)
    {
        try
        {

            List<Order> orders = _order.GetOrders();
            orders = searchString == 0 ? orders : orders.Where(o => o.OrderId == searchString).ToList();
            orders = status == "All Status" ? orders : orders.Where(o => o.Orderstatus == status).ToList();
            if (time != "All Time")
            {
                orders = GetOrdersByTime(time, orders);
            }
            List<OrdersElementView> ordersElementViews = new List<OrdersElementView> { };
            ordersElementViews = GetOrdersElementViewsFromOrders(orders);

            // Starting row for data (adjust based on your template)
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/templates/OrderTemplate.xlsx");
            using var workbook = new XLWorkbook(path);
            IXLWorksheet worksheet = workbook.Worksheet("Orders");

            // Set Filters in Excel
            worksheet.Cell(2, 3).Value = status;
            worksheet.Cell(2, 10).Value = searchString;
            worksheet.Cell(5, 3).Value = time;
            worksheet.Cell(5, 10).Value = ordersElementViews.Count();

            int row = 10;
            foreach (var order in ordersElementViews)
            {
                int col = 1;
                worksheet.Cell(row, col).Value = string.Concat("#", order.order_id);
                worksheet.Cell(row, col++).Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);

                worksheet.Cell(row, col).Value = order.date.ToShortDateString();
                worksheet.Range(worksheet.Cell(row, col), worksheet.Cell(row, col += 2)).Merge().Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);

                worksheet.Cell(row, ++col).Value = order.customer;
                worksheet.Range(worksheet.Cell(row, col), worksheet.Cell(row, col += 2)).Merge().Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);

                worksheet.Cell(row, 8).Value = order.status;
                worksheet.Range(worksheet.Cell(row, ++col), worksheet.Cell(row, col += 2)).Merge().Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);

                worksheet.Cell(row, ++col).Value = order.payment_mode;
                worksheet.Range(worksheet.Cell(row, col), worksheet.Cell(row, col += 1)).Merge().Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);

                worksheet.Cell(row, ++col).Value = order.rating;
                worksheet.Range(worksheet.Cell(row, col), worksheet.Cell(row, col += 1)).Merge().Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);

                worksheet.Cell(row, ++col).Value = order.total_amount;
                worksheet.Range(worksheet.Cell(row, col), worksheet.Cell(row, col += 1)).Merge().Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);

                row++;
            }

            // Convert workbook to memory stream
            MemoryStream stream = new();
            workbook.SaveAs(stream);
            stream.Position = 0;

            return stream; // No Response<> wrapper

        }
        catch (Exception e)
        {
            return new MemoryStream { };
        }
    }

}