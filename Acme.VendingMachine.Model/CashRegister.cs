using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        public static CashRegister Combine(CashRegister register1, CashRegister register2)
        {
            CashRegister newCashRegister = new CashRegister(CashDenomination.ALL_DONOMINATIONS);

            foreach (CashDenomination d in CashDenomination.ALL_DONOMINATIONS)
            {
                CashSet set = newCashRegister.CashSets.FirstOrDefault(s => s.Denomination.Amount == d.Amount);

                CashSet s1 = register1.CashSets.FirstOrDefault(s => s.Denomination.Amount == d.Amount);
                if (s1 != null)
                {
                    set.Quantity = set.Quantity + s1.Quantity;
                }

                CashSet s2 = register2.CashSets.FirstOrDefault(s => s.Denomination.Amount == d.Amount);
                if (s2 != null)
                {
                    set.Quantity = set.Quantity + s2.Quantity;
                }
            }

            return newCashRegister;
        }

        public void Sort(ListSortDirection direction)
        {
            if (direction == ListSortDirection.Ascending)
            {
                CashSets = CashSets.OrderBy(s => s.Denomination.Amount).ToList();
            }
            else
            {
                CashSets = CashSets.OrderBy(s => -s.Denomination.Amount).ToList();
            }
        }
    }
}
