using Acme.VendingMachine.DataAccess;
using Acme.VendingMachine.Model;

namespace Acme.VendingMachine.BusinessLogic
{
    public class MachineBll : IMachineBll
    {
        private readonly MachineDal _dal = null;

        public MachineBll()
        {
            _dal = new MachineDal(new ProductDal());
        }

        public Machine GetMachine()
        {
            return _dal.Get();
        }
    }
}
