using System;

namespace Acme.VendingMachine.Model
{
    public class Transaction
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }

        public int AmountDue
        {
            get
            {
                int due = 0;

                if (Product != null && Quantity > 0 && Quantity <= Product.Quantity)
                {
                    due = Product.Price * Quantity;
                }

                return due;
            }
        }

        private PaymentMethod _paymentMethod;
        public PaymentMethod PaymentMetthod
        {
            get
            {
                return _paymentMethod;
            }
        }

        public void SelectPaymentMethod(PaymentMethod paymentMethod)
        {
            _paymentMethod = paymentMethod;
        }

        // TODO: need a uniitified interface
        public IPayment Payment { get; set; }

        public bool Validate()
        {
            return Payment.Validate();
        }

        public int TotalAmountDue
        {
            get
            {
                return CalculateTotalAmountDue();
            }
        }

        private int CalculateTotalAmountDue()
        {
            int amountDue = AmountDue;

            if (Payment != null)
            {
                amountDue = Payment.CalculateTotalAmountDue(AmountDue);
            }

            return amountDue;
        }

        public void Confirm()
        {
            int amountPaid = Payment.MakePayment(TotalAmountDue);
        }
    }
}
