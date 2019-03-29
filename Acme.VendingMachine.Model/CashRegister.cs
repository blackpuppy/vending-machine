using System.Collections.Generic;
using System.Linq;

namespace Acme.VendingMachine.Model
{
    public class CashRegister
    {
        public IList<CashSet> CashSets = new List<CashSet>();

        public int Amount => CashSets.Sum(s => s.Amount);
    }
}
