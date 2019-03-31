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

        public void EnterNumber(string number)
        {
            Machine machine = _dal.Get();
            machine.AddInput(number);
        }

        public void Delete()
        {
            Machine machine = _dal.Get();
            machine.DeleteInput();
        }

        public void Clear()
        {
            Machine machine = _dal.Get();
            machine.ClearInput();
        }

        public void Done()
        {
            Machine machine = _dal.Get();
            machine.Done();
        }

        public void Cancel()
        {
            Machine machine = _dal.Get();
            machine.Cancel();
        }
    }
}
