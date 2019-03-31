using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Acme.VendingMachine.Model
{
    public class CashDenomination
    {
        #region Cash denominations

        public static readonly CashDenomination NICKEL = new CashDenomination() { Amount = 5, Name = "Nickel" };
        public static readonly CashDenomination DIME = new CashDenomination() { Amount = 10, Name = "Dime" };
        public static readonly CashDenomination QUARTER = new CashDenomination() { Amount = 25, Name = "Quarter" };
        public static readonly CashDenomination DOLLAR = new CashDenomination() { Amount = 100, Name = "Dollar" };
        public static readonly CashDenomination FIVE_DOLLAR = new CashDenomination() { Amount = 500, Name = "Five Dollar" };

        public static readonly IList<CashDenomination> ALL_DONOMINATIONS = 
            new List<CashDenomination>(new CashDenomination[] {
                NICKEL, DIME, QUARTER, DOLLAR, FIVE_DOLLAR
            });

        #endregion

        [Display(Name = "Denomination")]
        public string Name { get; set; }

        public int Amount { get; set; }
    }
}
