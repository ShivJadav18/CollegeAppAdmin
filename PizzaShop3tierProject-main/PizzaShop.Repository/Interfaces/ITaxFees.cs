using PizzaShop.Repository.Data;
using PizzaShop.Repository.ViewModels;

namespace PizzaShop.Repository.Interfaces;

public interface ITaxFees{
    public Message AddTax(Tax tax);
    public Tax GetTaxById(int taxid);
    public Message DeleteTax(int taxid);
    public List<Tax> GetTaxes();
    public Message EditTax(EditTaxViewModel editedTax);
     public Message ValidationBeforeAdd(string taxname);
}