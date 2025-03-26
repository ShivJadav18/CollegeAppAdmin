using Microsoft.EntityFrameworkCore;
using PizzaShop.Repository.Data;
using PizzaShop.Repository.Interfaces;
using PizzaShop.Repository.ViewModels;

namespace PizzaShop.Repository.Implementations;

public class Menu : IMenu{

    private readonly PizzaShopDbContext _context;

    public Menu(PizzaShopDbContext context){
        _context = context;
    }

    public void AddCategory(Category category){
        _context.Add(category);
        _context.SaveChanges();
    }

    public List<Category> GetCategories(){
        var categories = _context.Categories.Where(c => c.Isdeleted == false).OrderBy(c => c.CategoryId).ToList();

        return categories;
    }

    public void UpdateCategory(Category category){
        _context.Update(category);
        _context.SaveChanges();
    }

    public void RemoveCategory(Category category){
       category.Isdeleted = true;
        _context.SaveChanges();
    }

    public List<Item> GetItems(int categoryid,string searchval){
        var items = _context.Items.Where(i => (i.CategoryId == categoryid && string.IsNullOrEmpty(searchval) && i.Isdeleted == false) || (i.CategoryId == categoryid && i.Name.ToLower().Contains(searchval)  && i.Isdeleted == false)).OrderBy(i => i.ItemId).ToList();
        return items;
    }

    public Message DeleteItem(int itemid){
        try{
            Item item = GetItem(itemid);
            item.Isdeleted = true;
            _context.SaveChanges();
            // Message message = DeleteInItemToGroupByitemId(itemid);
            // if(message.error){
            //     return message;
            // }
            return new Message{error = false , errorMessage = "Item is successfully Deleted."};
        }catch(Exception e){
            return new Message{error = true,errorMessage = e.Message};
        }
    }

    public Message AddItem(Item item){
        try{
        _context.Add(item);
        _context.SaveChanges();
        return new Message{error = false};
        
        }catch(Exception e){
            return new Message{error = true,errorMessage = "Internal Error"};
        }

    }

    public Message UpdateItem(NewItem item){
        try{
            Item realitem = _context.Items.FirstOrDefault(i => i.ItemId == item.itemid);

            realitem.Name = item.Name;
            realitem.Description = item.Description;
            realitem.CategoryId = item.CategoryId;
            realitem.Defaulttax = item.Defaulttax;
            realitem.UnitId = item.UnitId;
            realitem.Rate = item.Rate;
            realitem.Quantity = item.Quantity;
            realitem.Taxpercentage = item.Taxpercentage;
            realitem.Itemtype = item.Itemtype;
            realitem.Imageurl = item.Imageurl;
            realitem.Isavailable = item.Isavailable;
            realitem.Shortcode = item.Shortcode;
            realitem.Updatedby = item.Updatedby;
            realitem.Updatedat = item.Updatedat;

            _context.SaveChanges();
            return new Message{error = false};
        }catch(Exception e){
        return new Message{error = true, errorMessage = "Some Internal Error."};
        }
    }

    public Item GetItem(int itemid){
        try{
        Item item = _context.Items.FirstOrDefault(i => i.ItemId == itemid);
         return item;
        }catch(Exception e){
            return new Item{};
        }
    }

    public Message DeleteMultipleItems(List<int> ids){

        try{

            foreach(int id in ids){
                Item item = GetItem(id);

                if(item != null){
                    item.Isdeleted = true;
                    DeleteInItemToGroupByitemId(id);
                }

            }

            _context.SaveChanges();
            return new Message{error = false , errorMessage = "Items are successfully deleted."};

        }catch(Exception e){
            return new Message{error = true, errorMessage = e.Message};
        }

    }

    public List<Modifiergroup> GetModifierGroups(){
        try{

            var ModifierGroups = _context.Modifiergroups.Where(m => m.Isdeleted == false).OrderBy(m => m.ModifiergroupId).ToList();

            return ModifierGroups;

        }catch(Exception e){
            return new List<Modifiergroup>{};
        }
    }

    public Modifiergroup GetModifiergroupById(int modifiergroupid){

        try{
            Modifiergroup modifiergroup = _context.Modifiergroups.FirstOrDefault(m => m.ModifiergroupId == modifiergroupid);

            if(modifiergroup == null){
                return new Modifiergroup{};
            }

            return modifiergroup;
        }catch(Exception e){
            return new Modifiergroup{};
        }

    }

    public List<Modifier> GetModifiersFromGroupIds(int ModifiergroupId,string searchval){

        try{
            List<int> modifiersids = _context.Modifiertomodifiergroups.Where(m => m.ModifiergroupId == ModifiergroupId && m.Isdeleted == false).Select(m => m.ModifierId).ToList();
            Modifier Modifiers1 = null;
            List<Modifier> modifiers = new List<Modifier>{};
            foreach(int id in modifiersids){
            Modifiers1 = _context.Modifiers.Include(m => m.Unit).FirstOrDefault(m => (string.IsNullOrEmpty(searchval) || m.Modifiername.ToLower().Contains(searchval))&& m.ModifierId == id && m.Isdeleted == false);
            if(Modifiers1 != null){
            modifiers.Add(Modifiers1);
            }
            Modifiers1 = null;
            }
            return modifiers;
        }catch(Exception e){
            return new List<Modifier> {};
        }

    }

    public Message AddModifierGroup(ModifierGroupViewModel modifierGroupViewModel){

        try{
            Modifiergroup modifiergroup = new Modifiergroup {
                Groupname = modifierGroupViewModel.Groupname,
                Description = modifierGroupViewModel.Description,
                Createdby = modifierGroupViewModel.Createdby,
                Updatedby = modifierGroupViewModel.Updatedby
            };
            _context.Add(modifiergroup);
            _context.SaveChanges();
            foreach(var modifierid in modifierGroupViewModel.modifiersIds){
                Modifiertomodifiergroup modifiertomodifiergroup = new Modifiertomodifiergroup{
                    ModifiergroupId = modifiergroup.ModifiergroupId,
                    ModifierId = modifierid
                };
                _context.Modifiertomodifiergroups.Add(modifiertomodifiergroup);
            }
          _context.SaveChanges();

            return new Message{error = false};
        }catch(Exception e){
            return new Message{error = true , errorMessage = e.Message};
        }
    }

    public Message AddModifier(Modifier modifier,int groupid){
        try{
            _context.Add(modifier);
            _context.SaveChanges();

            AddInModifierToGroup(modifier.ModifierId,groupid);

            return new Message{error = false};
        }catch(Exception e){
            return new Message{error = true,errorMessage = e.Message};
        }
    }

    public Message AddInModifierToGroup(int modifierid, int groupid){
        try{
            Modifiertomodifiergroup modifiertomodifiergroup = new Modifiertomodifiergroup{
                    ModifiergroupId = groupid,
                    ModifierId = modifierid
                };
                _context.Add(modifiertomodifiergroup);
                _context.SaveChanges();

                return new Message{error = false};
        }catch(Exception e){
            return new Message{error = true, errorMessage = e.Message};
        }
    }

    public Modifier GetModifierById(int modifierid){

        try{
            Modifier modifier = _context.Modifiers.FirstOrDefault(m => m.ModifierId == modifierid);

            if(modifier == null){
                return new Modifier{};
            }
            return modifier;
        }catch(Exception e){
            return new Modifier{};
        }
    }

    public Message UpdateModifier(ModifierViewModel modifierViewModel){
        try{
            Modifier modifier = GetModifierById((int)modifierViewModel.ModifierId);
            modifier.Modifiername = modifierViewModel.Modifiername;
            modifier.Description = modifierViewModel.Description;
            modifier.UnitId = modifierViewModel.UnitId;
            modifier.Rate = modifierViewModel.Rate;
            modifier.Quantity = modifierViewModel.Quantity;
            modifier.Updatedat = modifierViewModel.Updatedat;
            modifier.Updatedby = (int)modifierViewModel.Updatedby;
            _context.SaveChanges();

            Modifiertomodifiergroup modifiertomodifiergroup = _context.Modifiertomodifiergroups.FirstOrDefault(m => (m.ModifierId == modifier.ModifierId && m.ModifiergroupId == modifierViewModel.GroupId && m.Isdeleted == false));

            if(modifiertomodifiergroup == null){
                AddInModifierToGroup(modifier.ModifierId , modifierViewModel.GroupId);
            }

            return new Message{error = false };
        }catch(Exception e){
            return new Message{error = true, errorMessage = e.Message};
        }
    }

    public Message RemoveModifier(int modifierid){
        try{

            Modifier modifier = GetModifierById(modifierid);

            if(modifier == null ){
                return new Message{error = true, errorMessage = "Modifier does not exist."};
            }

            modifier.Isdeleted = true;

            _context.SaveChanges();

            IEnumerable<Modifiertomodifiergroup> modifiertomodifiergroups = _context.Modifiertomodifiergroups.Where(m => m.ModifierId == modifierid);

            foreach(Modifiertomodifiergroup modifiertomodifiergroup in modifiertomodifiergroups){
                modifiertomodifiergroup.Isdeleted = true;
            }
            _context.SaveChanges();
            return new Message{error = false};

        }catch(Exception e){
            return new Message { error = true , errorMessage = e.Message};
        }
    }

    public List<Modifier> GetAllModifiers(string searchval){

    List<Modifier>  Modifiers1 = _context.Modifiers.Include(m => m.Unit).Where(m => (string.IsNullOrEmpty(searchval) || m.Modifiername.Contains(searchval)) && m.Isdeleted == false).OrderBy(m => m.ModifierId).ToList();


    return Modifiers1;

    }

    public Message RemoveModifierGroup(int modifiergroupid){
    try{
        Modifiergroup modifiergroup = GetModifiergroupById(modifiergroupid);

        if(modifiergroup.ModifiergroupId == null){
            return new Message{error = true, errorMessage = "This Group does not exist." };    
        }

        modifiergroup.Isdeleted = true;

        _context.SaveChanges();

        IEnumerable<Modifiertomodifiergroup> modifiertomodifiergroups = _context.Modifiertomodifiergroups.Where(m => m.ModifiergroupId == modifiergroupid);

            foreach(Modifiertomodifiergroup modifiertomodifiergroup in modifiertomodifiergroups){
                modifiertomodifiergroup.Isdeleted = true;
            }
            _context.SaveChanges();

        Message message = DeleteInItemToGroupByGroupId(modifiergroupid);

        if(message.error){
            return message;
        }

        return new Message{error = false};

    }catch(Exception e) {
        return new Message{error = true, errorMessage = e.Message };
    }
    }

    public Message EditModifierGroup(ModifierGroupViewModel modifierGroupViewModel){

        try{
            Modifiergroup modifiergroup = GetModifiergroupById((int)modifierGroupViewModel.groupid);
            List<Modifiertomodifiergroup> modifiertomodifiergroups = _context.Modifiertomodifiergroups.Where(m => m.ModifiergroupId == (int)modifierGroupViewModel.groupid).ToList();
            modifiergroup.Description = modifierGroupViewModel.Description;
            modifiergroup.Groupname = modifierGroupViewModel.Groupname;
            _context.Modifiertomodifiergroups.RemoveRange(modifiertomodifiergroups);
            _context.SaveChanges();
            foreach( int id in modifierGroupViewModel.modifiersIds){ 
                    AddInModifierToGroup(id,(int)modifierGroupViewModel.groupid);
            }
            return new Message{error = false};
        }catch(Exception e) {
            return new Message{error = true, errorMessage = e.Message};
        }

    }

    public Message AddInItemToGroup(List<Itemtomodifiergroup> itemtomodifiergroups){

        try{
           
            foreach(Itemtomodifiergroup itemtomodifiergroup in itemtomodifiergroups){
                _context.Add(itemtomodifiergroup);
            }
            _context.SaveChanges();

            return new Message{error = false };
        }catch(Exception e){
            return new Message{error = true , errorMessage = e.Message };
        }

    }

    public List<Itemtomodifiergroup> GetItemtomodifiergroupsByItemId(int itemid){

        try{
            
            List<Itemtomodifiergroup> itemtomodifiergroups = _context.Itemtomodifiergroups.Where( i => i.ItemId == itemid && i.Isdeleted == false ).ToList();
            return itemtomodifiergroups;

        }catch(Exception e){
            return new List<Itemtomodifiergroup> {};
        }

    }

    public Message EditInItemToGroup(List<Itemtomodifiergroup> itemtomodifiergroups , int itemid){
        try{
            _context.Itemtomodifiergroups.Where( i => i.ItemId == itemid).ExecuteDelete();
            if(!itemtomodifiergroups.Any()){
                return new Message{ error = false , errorMessage = "Item is successfully edited."};
            }
          Message message = AddInItemToGroup(itemtomodifiergroups);

          if(message.error){
            return message;
          }
        
        return new Message{ error = false , errorMessage = "Item is successfully edited."};

        }catch(Exception e){
            return new Message{error = true , errorMessage = e.Message};
        }
    }

    public Message DeleteInItemToGroupByitemId(int itemid){

        try{

            IEnumerable<Itemtomodifiergroup> itemtomodifiergroups = _context.Itemtomodifiergroups.Where(i => i.ItemId == itemid);

            foreach(Itemtomodifiergroup itemtomodifiergroup in itemtomodifiergroups){
                itemtomodifiergroup.Isdeleted = true;
            }

            _context.SaveChanges();

            return new Message{ error = false };
        }catch(Exception e){
            return new Message{error = true , errorMessage = e.Message};
        }

    }

    public Message DeleteInItemToGroupByGroupId(int groupid){

        try{
            IEnumerable<Itemtomodifiergroup> itemtomodifiergroups = _context.Itemtomodifiergroups.Where(i => i.ModifiergroupId == groupid);

            foreach(Itemtomodifiergroup itemtomodifiergroup in itemtomodifiergroups){
                itemtomodifiergroup.Isdeleted = true;
            }

            _context.SaveChanges();

            return new Message{ error = false };
        }catch(Exception e){
            return new Message{error = true , errorMessage = e.Message};
        }

    }
}