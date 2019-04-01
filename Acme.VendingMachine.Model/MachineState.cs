namespace Acme.VendingMachine.Model
{
    public enum MachineState
    {
        SelectProduct,
        EnterQuantity,
        SelectPaymentMethod,
        CollectCash,
        EnterCreditCardNumber,
        ConfirmTransaction,
        Dispense,
    }
}
