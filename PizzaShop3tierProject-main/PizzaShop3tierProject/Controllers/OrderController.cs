using Microsoft.AspNetCore.Mvc;
using PizzaShop.Repository.Data;
using PizzaShop.Repository.ViewModels;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using PizzaShop.Service.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using SelectPdf;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using DocumentFormat.OpenXml.Bibliography;



namespace PizzaShop3tierProject.Controllers;

public class OrderController : Controller {

    private readonly IOrderService _orderService;
    private readonly ICompositeViewEngine _viewengine;
    private readonly ITempDataProvider _tempdata;
    public OrderController(IOrderService orderService,ICompositeViewEngine viewengine,ITempDataProvider tempdata){
        _viewengine = viewengine;
        _tempdata = tempdata;
        _orderService = orderService;
    }

    public IActionResult OrdersView(){
        OrdersPaginationView ordersPaginationView = new OrdersPaginationView{searchval = 0,status = "All Status",timefil = "All Time",from = null ,todate = null , order = 0, sortmethod = -1 };
        List<OrdersElementView> ordersElementViews = _orderService.GetOrdersElementViews(ordersPaginationView); 
       
            ordersPaginationView.ordersElementViews = ordersElementViews;
            ordersPaginationView.count = 5;
            ordersPaginationView.pageno = 1;
            ordersPaginationView.totalorders = ordersElementViews.Count();
        
        return View(ordersPaginationView);
    }

    public IActionResult GetOrdersForPagination(OrdersPaginationView ordersPaginationView){
        List<OrdersElementView> ordersElementViews = _orderService.GetOrdersElementViews(ordersPaginationView);
        OrdersPaginationView ordersPaginationView2 = new OrdersPaginationView{
            ordersElementViews = ordersElementViews,
            count = ordersPaginationView.count,
            pageno = ordersPaginationView.pageno,
            totalorders = ordersElementViews.Count()
        };
        return PartialView("_OrderTable",ordersPaginationView2);
    }

    // public IActionResult GetOrdersForSearch(DateTime from , DateTime to){
    //     List<OrdersElementView> orders = _orderService.GetOrdersForSearch(from,to);

    //     OrdersPaginationView ordersPaginationView2 = new OrdersPaginationView{
    //         ordersElementViews = orders,
    //         count = 5,
    //         pageno = 1,
    //         totalorders = orders.Count()
    //     };
        
    //     return PartialView("_OrderTable",ordersPaginationView2);
    // }

    public IActionResult ForExportExcel(string time ,string status,int searchString){

        MemoryStream fileStream = _orderService.ExportService(time,status,searchString);
        return File(fileStream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Orders.xlsx");
    }

    public IActionResult OrderPdfView(){
        return View();
    }

    public async Task<IActionResult> ForPdfDownload(){

       var viewHtml = await RenderViewToString("OrderPdfView");

        // Convert HTML to PDF
        HtmlToPdf converter = new HtmlToPdf();
        
        // Customize PDF settings
        converter.Options.PdfPageSize = PdfPageSize.A4;
        converter.Options.PdfPageOrientation = PdfPageOrientation.Portrait;
        converter.Options.MarginTop = 20;
        converter.Options.MarginBottom = 20;
        converter.Options.MarginLeft = 20;
        converter.Options.MarginRight = 20;

        
        // Create PDF document
        PdfDocument doc = converter.ConvertHtmlString(viewHtml);

        // Save PDF to a memory stream
        MemoryStream ms = new MemoryStream();
        doc.Save(ms);
        doc.Close();

        // Return the PDF file
        return File(ms.ToArray(), "application/pdf", "Report.pdf");
         
    }

     private async Task<string> RenderViewToString(string viewName)
    {
        using (var sw = new StringWriter())
        {
            var viewEngine = HttpContext.RequestServices.GetService(typeof(ICompositeViewEngine)) as ICompositeViewEngine;
            
            var viewResult = viewEngine.FindView(ControllerContext, viewName, false);

            if (viewResult.View == null)
            {
                throw new ArgumentNullException($"View {viewName} not found");
            }

            var viewContext = new ViewContext(
                ControllerContext,
                viewResult.View,
                ViewData,
                TempData,
                sw,
                new HtmlHelperOptions()
            );
           
            viewResult.View.RenderAsync(viewContext);
            return sw.ToString();
           
        }
    }
     
}

 