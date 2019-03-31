using System.Collections.Generic;
using Acme.VendingMachine.Model;

namespace Acme.VendingMachine.BusinessLogic
{
    public interface IMachineBll
    {
        Machine GetMachine();
        void EnterNumber(string number);
        void Delete();
        void Clear();
        void Done();
        void Cancel();
        void CollectCash(IList<CashSet> cashSets);
    }
}
