using System;
using System.Linq;
using System.ComponentModel;

namespace Acme.VendingMachine.Model
{
    public class CashPayment : IPayment
    {
        private CashRegister _cashInHands;
        private CashRegister _cashReceived;
        private int _amountDue;

        private CashRegister _cashChange = null;
        public CashRegister CashChange
        {
            get
            {
                return _cashChange;
            }
        }

        public CashPayment(CashRegister cashInHand, CashRegister cashReceived)
        {
            _cashInHands = cashInHand;
            _cashReceived = cashReceived;
        }

        public int CalculateTotalAmountDue(int amountDue)
        {
            _amountDue = amountDue;
            return amountDue;
        }

        public int MakePayment(int amountDue)
        {
            if (!Validate())
            {
                throw new Exception("Not Enoough Cash Received");
            }

            _amountDue = amountDue;

            // calculate change
            CalculateChange();

            return _cashReceived.Amount - (_cashChange == null ? 0 : _cashChange.Amount);
        }

        public bool Validate()
        {
            return _cashReceived.Amount >= _amountDue;
        }

        public void CalculateChange()
        {
            // combine cash in hand and cash received
            CashRegister newCashInHand = CashRegister.Combine(_cashInHands, _cashReceived);

            _cashChange = new CashRegister(CashDenomination.ALL_DONOMINATIONS);
            int change = _cashReceived.Amount - _amountDue;

            // deduct from max to min denominations as needed
            newCashInHand.Sort(ListSortDirection.Descending);
            foreach (CashSet fromSet in newCashInHand.CashSets)
            {
                if (change > fromSet.Denomination.Amount)
                {
                    int qty = (int)(change / fromSet.Denomination.Amount);
                    if (qty > fromSet.Quantity)
                    {
                        qty = fromSet.Quantity;
                    }

                    fromSet.Quantity = fromSet.Quantity - qty;

                    CashSet toSet = _cashChange.CashSets.First(s => s.Denomination.Amount == fromSet.Denomination.Amount);
                    toSet.Quantity = toSet.Quantity + qty;

                    change -= fromSet.Denomination.Amount * qty;
                }

                if (change <= 0)
                {
                    break;
                }
            }

            // if still remained, deduct from the min to max denomination to make it zero
            if (change > 0)
            {
                newCashInHand.Sort(ListSortDirection.Ascending);
                foreach (CashSet fromSet in newCashInHand.CashSets)
                {
                    int qty = (int)(change / fromSet.Denomination.Amount) + 1;
                    if (qty > fromSet.Quantity)
                    {
                        qty = fromSet.Quantity;
                    }

                    fromSet.Quantity = fromSet.Quantity - qty;

                    CashSet toSet = _cashChange.CashSets.First(s => s.Denomination.Amount == fromSet.Denomination.Amount);
                    toSet.Quantity = toSet.Quantity + qty;

                    change -= fromSet.Denomination.Amount * qty;

                    if (change <= 0)
                    {
                        break;
                    }
                }
            }
        }
    }
}
