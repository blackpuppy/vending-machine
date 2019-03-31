using System.Collections.Generic;
using System.Linq;

namespace Acme.VendingMachine.Model
{
    public class CashRegister
    {
        public CashRegister(IList<CashDenomination> denominations, int quantity = 0)
        {
            foreach (var d in denominations)
            {
                CashSets.Add(
                new CashSet() { Denomination = d, Quantity = quantity }
                );
            }
        }

        public IList<CashSet> CashSets = new List<CashSet>();

        public int Amount => CashSets.Sum(s => s.Amount);
    }
}
