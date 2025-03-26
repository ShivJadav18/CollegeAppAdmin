

using PizzaShop.Repository.Data;
using PizzaShop.Repository.ViewModels;

namespace PizzaShop.Repository.Interfaces;

public interface ITableandSection{

    public List<Section> GetSections();

    public Message AddSection(Section section);

    public Section GetSectionById(int sectionid);

    public Message EditSection(Section tempsection);

    public Message RemoveSection(int sectionid);
    public List<Table> GetTables(string searchval , int sectionid);

    public Message RemoveTable(int tableid);
    public Table GetTableById(int tableid);
    public Message AddTable(Table table);
    public Message EditTable(NewTable editedTable);
}