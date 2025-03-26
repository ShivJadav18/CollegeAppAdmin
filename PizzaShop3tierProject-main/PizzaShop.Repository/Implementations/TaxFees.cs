using PizzaShop.Repository.ViewModels;
using PizzaShop.Repository.Data;
using PizzaShop.Repository.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;

namespace PizzaShop.Repository.Implementations;

public class TaxFees:ITaxFees{

    private readonly PizzaShopDbContext _context;

    public TaxFees(PizzaShopDbContext context){
        _context = context;
    }

    public Message ValidationBeforeAdd(string taxname){
        try{
            Tax tax1 = _context.Taxes.FirstOrDefault( t => t.Name.ToLower() == taxname.ToLower() && t.Isdeleted == false);
            if(tax1 == null){
                return new Message{error = false};
            }
            return new Message{error = true , errorMessage = "This Tax name is already used."};
        }catch(Exception e){
            return new Message{ error = true , errorMessage = e.Message};
        }
    }
    public Message ValidationBeforeEdit(EditTaxViewModel editTax){
        try{
            Tax tax1 = _context.Taxes.FirstOrDefault( t => t.Name.ToLower() == editTax.Name.ToLower() && t.TaxId != editTax.TaxId && t.Isdeleted == false);
            if(tax1 == null){
                return new Message{error = false};
            }
            return new Message{error = true , errorMessage = "This Tax name is already used."};
        }catch(Exception e){
            return new Message{ error = true , errorMessage = e.Message};
        }
    }
    public Message AddTax(Tax tax){
        try{
            Message message = ValidationBeforeAdd(tax.Name);
            if(message.error){
                return message;
            }
            _context.Add(tax);
            _context.SaveChanges();
            return new Message{ error = false };
        }catch(Exception e){
           return new Message{ error = true , errorMessage = e.Message };
        }
    }

    public List<Tax> GetTaxes(){
        try{
            List<Tax> taxes = _context.Taxes.Where(t => t.Isdeleted == false ).OrderBy(t => t.TaxId).ToList();
            return taxes;
        }catch(Exception e){
            return new List<Tax>{};
        }
    }
    public Tax GetTaxById(int taxid){
        try{
            Tax tax = _context.Taxes.First(t => t.TaxId == taxid && t.Isdeleted == false );
            if(tax == null){
                return new Tax{};
            }
            return tax;
        }catch(Exception e){
            return new Tax{};
        }
    }
    public Message DeleteTax(int taxid){
        try{
            Tax tax = GetTaxById(taxid);
            if(tax.TaxId == null){
                return new Message{ error = true , errorMessage = "Some Internal error." };
            }
            tax.Isdeleted = true;
            _context.SaveChanges();
            return new Message{error = false};
        }catch(Exception e){
            return new Message{ error = true , errorMessage = e.Message };
        }
    }

    public Message EditTax(EditTaxViewModel editedTax){
        try{
            Message message = ValidationBeforeEdit(editedTax);
            if(message.error){
                return message;
            }
            Tax tax = GetTaxById(editedTax.TaxId);
            tax.Updatedat = editedTax.Updatedat;
            tax.Updatedby = editedTax.Updatedby;
            tax.Amount = editedTax.Amount;
            tax.Name = editedTax.Name;
            tax.Isdefault = editedTax.Isdefault;
            tax.Isenabled = editedTax.Isenabled;
            tax.Taxtype = editedTax.Taxtype;
            _context.SaveChanges();
            return new Message{error = false};
        }catch(Exception e){
            return new Message{ error = true , errorMessage = e.Message }; 
        }
    }

}