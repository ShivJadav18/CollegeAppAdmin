using PizzaShop.Repository.Data;
using PizzaShop.Repository.ViewModels;

namespace PizzaShop.Service.Interfaces;

public interface ITaxServices{
    public List<Tax> GetTaxesService();
    public Message AddTaxService(Tax tax);
    public Tax GetTaxForEdit(int taxid);
    public Message EditTaxService(EditTaxViewModel editedTax);
    public Message DeleteTaxService(int taxid);
}