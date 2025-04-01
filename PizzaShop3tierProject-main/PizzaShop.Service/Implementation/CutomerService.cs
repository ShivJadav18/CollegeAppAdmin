using ClosedXML.Excel;
using Microsoft.IdentityModel.Tokens;
using PizzaShop.Repository.Data;
using PizzaShop.Repository.Interfaces;
using PizzaShop.Repository.ViewModels;
using PizzaShop.Service.Interfaces;

namespace PizzaShop.Service.Implementation;

public class CustomerService:ICustomerService{
    private readonly ICustomer _customer;
    public CustomerService(ICustomer customer){
        _customer = customer;
    }

    public List<Customer> GetCustomersService(){
        List<Customer> customers = _customer.GetAllCustomers();
        return customers;
    }

    private List<CustomerView> GetCustomerViewsForPagination(CustomersPaginationView customersPaginationViewin){
        try{
            
            List<Customer> customers = GetCustomersService();

            if(!customersPaginationViewin.searchval.IsNullOrEmpty()){
                customers = customers.Where(c => c.Firstname.ToLower().Contains(customersPaginationViewin.searchval.ToLower())).ToList();
            }

            List<CustomerView> customerViews = GetCustomerViewsFromCustomers(customers);

            if(customersPaginationViewin.timefilter != "All Time" ){
                List<CustomerView> customerViews1 = new List<CustomerView>{};
            if(customersPaginationViewin.timefilter == "Custom Date"){
                if(!customersPaginationViewin.from.Equals(DateTime.MinValue) && !customersPaginationViewin.to.Equals(DateTime.MinValue)){
                    foreach(CustomerView customerView in customerViews){
                        DateTime orderDate = customerView.OrderDate;
                        if(orderDate.Date >= customersPaginationViewin.from.Date && orderDate.Date <= customersPaginationViewin.to.Date){
                            customerViews1.Add(customerView);
                        }
                    }
                    customerViews = customerViews1;
                }
            }else{
                customerViews = GetCustomersByTime(customersPaginationViewin.timefilter,customerViews);
            }
            }

            
            return customerViews;
        }catch(Exception e){
            return new List<CustomerView>{};
        }
    }

    public CustomersPaginationView GetCustomersPaginationView(CustomersPaginationView customersPaginationViewin){
        try{
            CustomersPaginationView customersPaginationView = new CustomersPaginationView{};
            List<CustomerView> customerViews = GetCustomerViewsForPagination(customersPaginationViewin);
            if(customersPaginationViewin.sortmethod != -1 && customersPaginationViewin.sortval != -1){
                var sortmethod = customersPaginationViewin.sortmethod;
                var sortval = customersPaginationViewin.sortval;

                if(sortval == 1){
                    if(sortmethod == 0){
                        customerViews = customerViews.OrderBy(c => c.Firstname).ToList();
                    }else{
                        customerViews = customerViews.OrderByDescending(c => c.Firstname).ToList();
                    }
                }else if(sortval == 2){
                    if(sortmethod == 0){
                        customerViews = customerViews.OrderBy(c => c.OrderDate).ToList();
                    }else{
                        customerViews = customerViews.OrderByDescending(c => c.OrderDate).ToList();
                    }
                }else{
                    if(sortmethod == 0){
                        customerViews = customerViews.OrderBy(c => c.totalOrders).ToList();
                    }else{
                        customerViews = customerViews.OrderByDescending(c => c.totalOrders).ToList();
                    }
                }
            }
            List<CustomerView> customerViews2 = customerViews.Skip((customersPaginationViewin.pageno - 1) * customersPaginationViewin.count).Take(customersPaginationViewin.count).ToList();
            customersPaginationView.totalCustomers = customerViews.Count;
            customersPaginationView.customers = customerViews2;
            customersPaginationView.count = customersPaginationViewin.count;
            customersPaginationView.pageno = customersPaginationViewin.pageno;
            return customersPaginationView;
        }catch(Exception e){
            return new CustomersPaginationView{};
        }
    }

    private List<CustomerView> GetCustomersByTime(string timefilter , List<CustomerView> customers){
         DateTime time = new DateTime();
        if (timefilter == "Last 7 days")
        {
            time = DateTime.UtcNow.AddDays(-7);
            customers = customers.Where(customer => customer.OrderDate >= time.Date).ToList();
        }
        else if (timefilter == "Last 30 days")
        {
            time = DateTime.UtcNow.AddDays(-30);
            customers = customers.Where(customer => customer.OrderDate >= time.Date).ToList();
        }else if(timefilter == "Today"){
            time = DateTime.Now.Date;
            customers = customers.Where(customer => customer.OrderDate >= time.Date).ToList();
        }else 
        {
            var startOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            var startOfNextMonth = startOfMonth.AddMonths(1);
            customers = customers.Where(customer => customer.OrderDate >= startOfMonth.Date && customer.OrderDate <= startOfNextMonth).ToList();
        }

        return customers;
    }

    public List<CustomerView> GetCustomerViewsFromCustomers(List<Customer> customers){
        List<CustomerView> customerViews = new List<CustomerView>{};
            foreach(Customer customer in customers){
                CustomerView customerView = new CustomerView{
                    CustomerId = customer.CustomerId,
                    Firstname = customer.Firstname,
                    Email = customer.Email,
                    Contactnumber = customer.Contactnumber,
                    OrderDate = customer.Orders.Count == 0 ? (DateTime)customer.Createdat : (DateTime) customer.Orders.OrderBy(o => o.Orderdate).Last().Orderdate,
                    totalOrders = customer.Orders.Count
                };
                customerViews.Add(customerView);
            }
            return customerViews;
    }

    public MemoryStream ExportCustomersService(CustomersPaginationView customersPaginationViewIn){
        try{
        List<CustomerView> customersPaginationViews = GetCustomerViewsForPagination(customersPaginationViewIn);

         // Starting row for data (adjust based on your template)
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/templates/CustomerTemplate.xlsx");
            using var workbook = new XLWorkbook(path);
            IXLWorksheet worksheet = workbook.Worksheet("Customers");

            // Set Filters in Excel
            worksheet.Cell(2, 3).Value = "";
            worksheet.Cell(2, 10).Value = customersPaginationViewIn.searchval;
            worksheet.Cell(5, 3).Value = customersPaginationViewIn.timefilter;
            worksheet.Cell(5, 10).Value = customersPaginationViews.Count();

            int row = 10;
            foreach (var customer in customersPaginationViews)
            {
                int col = 1;
                worksheet.Cell(row, col).Value = string.Concat("#", customer.CustomerId);
                worksheet.Cell(row, col++).Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);

                worksheet.Cell(row, col).Value = customer.Firstname;
                worksheet.Range(worksheet.Cell(row, col), worksheet.Cell(row, col += 2)).Merge().Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);

                worksheet.Cell(row, ++col).Value = customer.Email;
                worksheet.Range(worksheet.Cell(row, col), worksheet.Cell(row, col += 2)).Merge().Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);

                worksheet.Cell(row, ++col).Value = customer.OrderDate.ToShortDateString();
                worksheet.Range(worksheet.Cell(row, col), worksheet.Cell(row, col += 2)).Merge().Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);

                worksheet.Cell(row, ++col).Value = customer.Contactnumber;
                worksheet.Range(worksheet.Cell(row, col), worksheet.Cell(row, col += 3)).Merge().Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);

                worksheet.Cell(row, ++col).Value = customer.totalOrders;
                worksheet.Range(worksheet.Cell(row, col), worksheet.Cell(row, col += 1)).Merge().Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);

                row++;
            }

            // Convert workbook to memory stream
            MemoryStream stream = new();
            workbook.SaveAs(stream);
            stream.Position = 0;

            return stream; // No Response<> wrapper
        }catch(Exception e){
            return new MemoryStream();
        }
    }

}