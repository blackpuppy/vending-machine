using System;

namespace Acme.VendingMachine.Model
{
    public class CreditCardPayment : IPayment
    {
        private const int VALID_CARD_DIGITS = 4; // for testing

        private string _creditCardNumber;

        public CreditCardPayment(string creditCardNumber)
        {
            _creditCardNumber = creditCardNumber;
        }

        public int CalculateTotalAmountDue(int amountDue)
        {
            return (int)(amountDue * 1.05);
        }

        public bool Validate()
        {
            return ValidateCreditCard();
        }

        public int MakePayment(int totalAmountDue)
        {
            if (!ValidateCreditCard())
            {
                throw new Exception("Invalid Credit Card");
            }

            // deduct amount due + 5% transaction fee
            return totalAmountDue;
        }

        private bool ValidateCreditCard()
        {
            return !string.IsNullOrEmpty(_creditCardNumber)
                && _creditCardNumber.Length == VALID_CARD_DIGITS;
        }
    }
}
