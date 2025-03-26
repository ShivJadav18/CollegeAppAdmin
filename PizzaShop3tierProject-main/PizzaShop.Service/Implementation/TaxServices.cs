using PizzaShop.Repository.Data;
using PizzaShop.Repository.Interfaces;
using PizzaShop.Repository.ViewModels;
using PizzaShop.Service.Interfaces;

namespace PizzaShop.Service.Implementation;

public class TaxServices : ITaxServices{

    private readonly ITaxFees _tax;

    public TaxServices(ITaxFees tax){
        _tax = tax;
    }

    public List<Tax> GetTaxesService(){
        List<Tax> taxes = _tax.GetTaxes();
        return taxes;
    }

    public Message AddTaxService(Tax tax){
        tax.Createdat = DateTime.Now;
        tax.Updatedat = DateTime.Now;

        Message message = _tax.AddTax(tax);

        return message;
    }

    public Tax GetTaxForEdit(int taxid){
        Tax tax = _tax.GetTaxById(taxid);
        return tax;
    }

    public Message EditTaxService(EditTaxViewModel editedTax){
        editedTax.Updatedat = DateTime.Now;
        Message message = _tax.EditTax(editedTax);
        return message;
    }

    public Message DeleteTaxService(int taxid){
        Message message = _tax.DeleteTax(taxid);
        return message;
    }

}