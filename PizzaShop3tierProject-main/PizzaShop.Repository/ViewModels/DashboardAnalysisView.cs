namespace PizzaShop.Repository.ViewModels;
public class DashboardAnalysisView{
    public ValueData valueData{get;set;}
    public BarChartData barChartData{get;set;}
    public List<ForPie> pievalues{get;set;}
}

public class ValueData{
    public decimal TotalSales{get;set;}
    public int TotalOrders{get;set;}
    public decimal AvgOrder{get;set;}
    public decimal TotalProfit{get;set;}
}

public class BarChartData{
    public int janPer{get;set;}
    public int febPer{get;set;}
    public int marPer{get;set;}
    public int aprPer{get;set;}
    public int mayPer{get;set;}
    public int junPer{get;set;}
    public int julPer{get;set;}
    public int augPer{get;set;}
    public int sepPer{get;set;}
    public int octPer{get;set;}
    public int novPer{get;set;}
    public int decPer{get;set;}
}

public class ForPie{
    public string companyName{get;set;}
    public decimal Percentage{get;set;}
}