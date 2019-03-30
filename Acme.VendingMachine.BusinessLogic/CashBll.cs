using Acme.VendingMachine.DataAccess;
using Acme.VendingMachine.Model;

namespace Acme.VendingMachine.BusinessLogic
{
    public class CashBll : ICashBll
    {
        private readonly CashDal _dal = null;

        public CashBll()
        {
            _dal = new CashDal();
        }

        public CashRegister GetAllCash()
        {
            return _dal.GetAllCash();
        }
    }
}
