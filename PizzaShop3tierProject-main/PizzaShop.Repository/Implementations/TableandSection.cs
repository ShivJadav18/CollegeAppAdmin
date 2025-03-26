
using System.Data;
using Microsoft.EntityFrameworkCore;
using PizzaShop.Repository.Data;
using PizzaShop.Repository.Interfaces;
using PizzaShop.Repository.ViewModels;

namespace PizzaShop.Repository.Implementations;

public class TableandSection : ITableandSection{

    private readonly PizzaShopDbContext _context;

    public TableandSection(PizzaShopDbContext context){
        _context = context;
    }

    public Message ValidationBeforeAdd(Table table){
        try{
            Table table1 = _context.Tables.FirstOrDefault(t => t.Name == table.Name && t.SectionId == table.SectionId && t.Isdeleted == false);
            if(table1 == null){
                return new Message{error = false};
            }
            return new Message{error = true ,errorMessage = "This Name already used."};
        }catch(Exception e){
            return new Message{error = true , errorMessage = e.Message};
        }
    }
    public List<Section> GetSections(){

        try{
            List<Section> sections = _context.Sections.Where(s => s.Isdeleted == false).OrderBy(s => s.SectionId).ToList();
            return sections;
        }catch(Exception e){
            return new List<Section>{};
        }
    }

    public Message AddSection(Section section){
        try{
            _context.Add(section);
            _context.SaveChanges();
            return new Message{ error = false};
        }catch(Exception e){
            return new Message{ error = true , errorMessage = e.Message};
        }
    }

    public Section GetSectionById(int sectionid){
        try{
            Section section = _context.Sections.FirstOrDefault(s => s.SectionId == sectionid && s.Isdeleted == false );
            if(section == null){
                  return new Section{};
            }
            return section;
        }catch(Exception e){
            return new Section{};
        }
    }

    public Message EditSection(Section tempsection){
        try{
            Section section = _context.Sections.FirstOrDefault(s => s.SectionId == tempsection.SectionId);
            if(section == null){
                return new Message{error = true, errorMessage = "There is some internal error."};
            }
            section.Updatedat = tempsection.Updatedat;
            section.Updatedby = tempsection.Updatedby;
            section.Name = tempsection.Name;
            section.Description = tempsection.Description;

            _context.SaveChanges();
            return new Message{error = false};
        }catch(Exception e){
            return new Message{error = true, errorMessage = e.Message};
        }
    }

    public Message RemoveSection(int sectionid){
        try{
            Section section = GetSectionById(sectionid);
            section.Isdeleted = true;
            _context.SaveChanges();
            return new Message{error = false};
        }catch(Exception e){
            return new Message{ error = true , errorMessage = e.Message };
        }
    }

    public List<Table> GetTables(string searchval , int sectionid){
        try{
            List<Table> tables = _context.Tables.Where(t => t.SectionId == sectionid && (string.IsNullOrEmpty(searchval) || t.Name.ToLower().Contains(searchval)) && t.Isdeleted == false).OrderBy(t => t.TableId).ToList();
            if(tables == null){
                return new List<Table>{};
            }
            return tables;
        }catch(Exception e){
            return new List<Table>{};
        }
    }

    public Table GetTableById(int tableid){
        try{
            Table table = _context.Tables.FirstOrDefault(t => t.TableId == tableid && t.Isdeleted == false);
            if(table == null){
                return new Table{};
            }
            return table;
        }catch(Exception e){
            return new Table{};
        }
    }

    public Message RemoveTable(int tableid){
        try{
            Table table = GetTableById(tableid);
            if(table.TableId == null ){
                return new Message{error = true , errorMessage = "Some internal error."};
            }
            table.Isdeleted = true;
            _context.SaveChanges();
            return new Message{error = false };
        }catch(Exception e){
            return new Message{error = true , errorMessage = e.Message };
        }
    }

    public Message AddTable(Table table){
        try{
            Message message = ValidationBeforeAdd(table);
            if(message.error){
                return message;
            }
            _context.Add(table);
            _context.SaveChanges();
            return new Message{error = false};
        }catch(Exception e){
            return new Message{error = true ,errorMessage = e.Message};
        }
    }

    public Message EditTable(NewTable editedTable){
        try{
            Table table = GetTableById((int)editedTable.tableid);
            if(table.TableId == null){
                return new Message{error = true , errorMessage = "Some Internal Error."};
            }
            table.Capacity = editedTable.Capacity;
            table.Name = editedTable.name;
            table.SectionId = editedTable.sectionid;
            table.Updatedat = editedTable.Updatedat;
            table.Updatedby = editedTable.Updatedby;
            table.Status = editedTable.status;

            _context.SaveChanges();
            return new Message{error = false};
        }catch(Exception e){
            return new Message{error = true ,errorMessage = e.Message};
        }
    }
}