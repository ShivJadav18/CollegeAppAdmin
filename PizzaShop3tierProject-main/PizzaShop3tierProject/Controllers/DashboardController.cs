
using PizzaShop.Repository.Data;
using PizzaShop.Repository.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using pizzashop.service.Attributes;
using PizzaShop.Service.Interfaces;


namespace PizzaShop3tierProject.Controllers{

    [CustomAuthorize]
    public class DashboardController : Controller {

        private readonly PizzaShopDbContext _context;
        private readonly IDashbordService _dashbordService;

        public DashboardController(PizzaShopDbContext context,IDashbordService dashbordService){
            _dashbordService = dashbordService;
            _context = context;

        }

        public async Task<IActionResult> Dashboardpage(){

            var token = Request.Cookies["jwtCookie"];
            DashboardAnalysisView dashboardAnalysisView = new DashboardAnalysisView{};

            if(token != null){
                ValueData valueData = _dashbordService.GetValueData();
                dashboardAnalysisView.valueData = valueData;
                dashboardAnalysisView.barChartData = _dashbordService.GetBarChartData();
                List<ForPie> pievalues = _dashbordService.GetListOfPieValues();
                dashboardAnalysisView.pievalues = pievalues;
                return View(dashboardAnalysisView);
            }

            return RedirectToAction("Login","Authenticate");

        }

    }

}