namespace Acme.VendingMachine.Model
{
    public interface IPayment
    {
        int CalculateTotalAmountDue(int amountDue);
        bool Validate();
        int MakePayment(int amountDue);
    }
}
