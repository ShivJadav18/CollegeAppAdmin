using PizzaShop.Repository.Data;
using PizzaShop.Repository.ViewModels;

namespace PizzaShop.Service.Interfaces;

public interface ITableandSectionService{


    public TableandSectionViewModel GetTableandSectionViewModel();

    public Message AddSectionService(string name ,string Description, int id);

    public Section GetSectionByIdService(int sectionid);

    public Message EditSection(Section section);

    public Message DeleteSectionService(int sectionid);

    public Tables GetTables(int count, string searchval , int pageno , int sectionid);
    public Message DeleteTableService(int tableid);
    public Message AddTableService(NewTable newTable);
    public Table GetTableByIdService(int tableid);
    public Message UpdateTable(NewTable editedTable);
     public Message DeleteMultipleTablesService(DeleteItemsViewModel tables);
}