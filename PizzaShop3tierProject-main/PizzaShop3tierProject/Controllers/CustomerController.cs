using Microsoft.AspNetCore.Mvc;
using PizzaShop.Repository.Data;
using PizzaShop.Repository.ViewModels;
using PizzaShop.Service.Interfaces;

namespace PizzaShop3tierProject.Controllers;

public class CustomerController:Controller{
    private readonly ICustomerService _customerService;

    public CustomerController(ICustomerService customerService){
        _customerService = customerService;
    }

    public IActionResult CustomerView(){
        CustomersPaginationView customersPaginationView = new CustomersPaginationView{searchval = "" , timefilter = "All Time"};
        customersPaginationView.count = 5;
        customersPaginationView.pageno = 1;
        customersPaginationView = _customerService.GetCustomersPaginationView(customersPaginationView);
        return View(customersPaginationView);
    }

    public IActionResult GetCustomersPaginationView(CustomersPaginationView customersPaginationView){
        CustomersPaginationView customersPaginationView1 = _customerService.GetCustomersPaginationView(customersPaginationView);
        return PartialView("_CustomersTable",customersPaginationView1);
    }

     public IActionResult ForExportExcel(string time,string searchString,DateTime from = new DateTime() , DateTime to = new DateTime()){

        CustomersPaginationView customersPaginationView = new CustomersPaginationView{
            searchval = searchString,
            timefilter = time,
            from = from,
            to = to
        };
        MemoryStream fileStream = _customerService.ExportCustomersService(customersPaginationView);

        return File(fileStream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Customers.xlsx");
    }

}