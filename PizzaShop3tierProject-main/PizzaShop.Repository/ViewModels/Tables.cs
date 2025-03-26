using PizzaShop.Repository.Data;

namespace PizzaShop.Repository.ViewModels;

public class Tables{

    public List<Table> tables{get; set;}

    public int count{get; set;}

    public int pageno {get; set;}

    public int totaltables{get; set;}

    public string searchval{get; set;}

    public int sectionid{get; set;}

}