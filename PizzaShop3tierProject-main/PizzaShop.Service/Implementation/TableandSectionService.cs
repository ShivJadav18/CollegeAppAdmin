using Microsoft.CodeAnalysis.CSharp.Syntax;
using PizzaShop.Repository.Data;
using PizzaShop.Repository.Interfaces;
using PizzaShop.Repository.ViewModels;
using PizzaShop.Service.Interfaces;

namespace PizzaShop.Service.Implementation;

public class TableandSectionService : ITableandSectionService{

    private readonly ITableandSection _tableandsection;

    public TableandSectionService ( ITableandSection tableandSection ){
        _tableandsection = tableandSection;
    }

    public TableandSectionViewModel GetTableandSectionViewModel(){

        List<Section> sections = _tableandsection.GetSections();
        Tables tables = GetTables(5,"",1,sections[0].SectionId);
        return new TableandSectionViewModel{sections = sections , tables = tables};

    }

    public Message AddSectionService(string name ,string Description,int id){

        Section section = new Section{Name = name , Description = Description ,Createdby = id , Updatedby = id};

        Message message = _tableandsection.AddSection(section);

        return message;
    }

    public Section GetSectionByIdService(int sectionid){
        Section section = _tableandsection.GetSectionById(sectionid);

        return section;
    }

    public Message EditSection(Section section){

        section.Updatedat = DateTime.Now;

        Message message = _tableandsection.EditSection(section);

        return message;
    }

    public Message DeleteSectionService(int sectionid){

        Message message = _tableandsection.RemoveSection(sectionid);

        return message;
    }

    public Tables GetTables(int count, string searchval , int pageno , int sectionid){

         List<Table> tables = _tableandsection.GetTables(searchval ,sectionid);

        List<Table> finaltables = tables.Skip((pageno - 1) * count).Take(count).ToList();

        Tables tables1 = new Tables
        {
            tables = finaltables,
            count = count,
            pageno = pageno,
            totaltables = tables.Count(),
            sectionid = sectionid
        };

        return tables1;
    }

    public Message DeleteTableService(int tableid){
        Message message = _tableandsection.RemoveTable(tableid);
        return message;
    }
    
    public Message AddTableService(NewTable newTable){

        Table table = new Table{
            Name = newTable.name,
            SectionId = newTable.sectionid,
            Status = newTable.status,
            Capacity = newTable.Capacity,
            Createdby = newTable.Createdby,
            Updatedby = newTable.Updatedby
        };

        Message message = _tableandsection.AddTable(table);

        return message;

    }

    public Table GetTableByIdService(int tableid){
        Table table = _tableandsection.GetTableById(tableid);
        return table;
    }

    public Message UpdateTable(NewTable editedTable){
        editedTable.Updatedat = DateTime.Now;
        Message message = _tableandsection.EditTable(editedTable);
        return message;
    }

    public Message DeleteMultipleTablesService(DeleteItemsViewModel tables){

        foreach(int id in tables.ids){
            Message message = _tableandsection.RemoveTable(id);
            if(message.error){
                return message;
            }
        }
        return new Message{error = false};
    }
}