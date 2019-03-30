using Acme.VendingMachine.Model;

namespace Acme.VendingMachine.BusinessLogic
{
    public interface ICashBll
    {
        CashRegister GetAllCash();
    }
}