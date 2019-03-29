namespace Acme.VendingMachine.Model
{
    /// <summary>
    /// Represents a set of cash notes or coins of the same denomination.
    /// </summary>
    public class CashSet
    {
        public CashDenomination Denomination { get; set; }
        public int Quantity { get; set; }

        public int Amount => Denomination.Amount * Quantity;
    }
}
