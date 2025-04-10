using PizzaShop.Repository.ViewModels;
namespace PizzaShop.Service.Interfaces;

public interface IDashbordService{
    public ValueData GetValueData();
    public BarChartData GetBarChartData();
    public List<ForPie> GetListOfPieValues();
}