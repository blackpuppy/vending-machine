using System;
using System.Collections.Generic;
using System.Text;

namespace Acme.VendingMachine.Model
{
    public class CashDenomination
    {
        #region Cash denominations

        public static readonly CashDenomination NICKEL = new CashDenomination() { Amount = 5 };
        public static readonly CashDenomination DIME = new CashDenomination() { Amount = 10 };
        public static readonly CashDenomination QUARTER = new CashDenomination() { Amount = 25 };
        public static readonly CashDenomination DOLLAR = new CashDenomination() { Amount = 100 };
        public static readonly CashDenomination FIVE_DOLLAR = new CashDenomination() { Amount = 500 };

        #endregion

        public int Amount { get; set; }
    }
}
