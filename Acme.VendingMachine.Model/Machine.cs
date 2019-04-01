using System;
using System.Collections.Generic;
using System.Linq;

namespace Acme.VendingMachine.Model
{
    public class Machine
    {
        public Machine(IList<Product> products, CashRegister cashInHand)
        {
            Products = products;
            CashInHand = cashInHand;

            CashReceived = new CashRegister(CashDenomination.ALL_DONOMINATIONS);
            CashChange = new CashRegister(CashDenomination.ALL_DONOMINATIONS);

            _messages = new List<string>();
            SwitchState(MachineState.SelectProduct);
        }

        public IList<Product> Products { get; set; }
        public Transaction Transaction { get; set; }

        public CashRegister CashInHand { get; set; }
        public CashRegister CashReceived { get; set; }
        public CashRegister CashChange { get; set; }

        #region State

        public MachineState State { get; set; }

        public void SwitchState(MachineState newState)
        {
            ClearInput();
            State = newState;

            switch (State)
            {
                case MachineState.SelectProduct:
                    ClearProductSelected();
                    CashReceived = new CashRegister(CashDenomination.ALL_DONOMINATIONS);
                    CashChange = new CashRegister(CashDenomination.ALL_DONOMINATIONS);
                    _messages.Add("Please select a product:");
                    break;
                case MachineState.EnterQuantity:
                    AddMessage("Please enter quantity:");
                    break;
                case MachineState.SelectPaymentMethod:
                    AddMessage("Please select a payment method (1-Cash 2-Credit Card):");
                    break;
                case MachineState.CollectCash:
                    AddMessage(string.Format("Please put cash in the cash collector for the amount Due: {0}", Transaction.AmountDue));
                    break;
                case MachineState.EnterCreditCardNumber:
                    AddMessage("Please enter credit card number:");
                    break;
                case MachineState.ConfirmTransaction:
                    AddMessage("Are you sure you want to make the purchase?");
                    break;
            }
        }

        public void Done()
        {
            switch (State)
            {
                case MachineState.SelectProduct:
                    SelectProduct();
                    break;
                case MachineState.EnterQuantity:
                    EnterQuantity();
                    break;
                case MachineState.SelectPaymentMethod:
                    SelectPaymentMethod();
                    break;
                case MachineState.EnterCreditCardNumber:
                    EnterCreditCard();
                    break;
                case MachineState.ConfirmTransaction:
                    ConfirmTransaction();
                    break;
                case MachineState.Dispense:
                    SwitchState(MachineState.SelectProduct);
                    break;
            }
        }

        public void Cancel()
        {
            if (Transaction.PaymentMetthod == PaymentMethod.Cash)
            {
                CashChange = CashReceived;
                AddMessage("Return cash received");
            }

            SwitchState(MachineState.Dispense);
        }

        #endregion

        #region Input

        public string Input { get; set; }

        public void AddInput(string number)
        {
            Input += number;
        }

        public void DeleteInput()
        {
            if (!string.IsNullOrEmpty(Input))
            {
                Input = Input.Substring(0, Input.Length - 1);
            }
        }

        public void ClearInput()
        {
            Input = null;
        }

        #endregion

        #region Message

        private IList<string> _messages { get; set; }

        public string DisplayMessage
        {
            get
            {
                int rowsToTake = string.IsNullOrEmpty(Input) ? 5 : 4;
                List<string> messagesToDisplay = _messages.TakeLast(rowsToTake).ToList();
                string message = string.Join(Environment.NewLine, messagesToDisplay);

                if (!string.IsNullOrEmpty(Input))
                {
                    message += " " + Input;
                }

                return message;
            }
        }

        public void AppendMessage(string message)
        {
            int count = _messages.Count;
            if (count > 0)
            {
                string lastMsg = _messages[_messages.Count - 1];
                _messages[_messages.Count - 1] = lastMsg + message;
            }
            else
            {
                AddMessage(message);
            }
        }

        public void AddMessage(string message)
        {
            _messages.Add(message);
        }

        #endregion

        #region Select Product

        public void SelectProduct()
        {
            if (State != MachineState.SelectProduct)
            {
                return;
            }

            AppendMessage(string.Format(" {0}", Input));

            Product selected = Products.FirstOrDefault(p => p.ItemNo == int.Parse(Input));
            if (selected == null)
            {
                AddMessage("Error: Product Not Found!");
                SwitchState(MachineState.SelectProduct);
            }
            else if (selected.Quantity == 0)
            {
                AddMessage("Error: Product Sold Out!");
                SwitchState(MachineState.SelectProduct);
            }
            else
            {
                ClearProductSelected();
                selected.Seleted = true;
                AddMessage(string.Format("# {0} {1} selected!", selected.ItemNo, selected.Name));

                EnsureTransaction();
                Transaction.Product = selected;

                SwitchState(MachineState.EnterQuantity);
            }
        }

        private void ClearProductSelected()
        {
            Products.All(p => p.Seleted = false);
        }

        #endregion

        #region Enter Quantity

        public void EnterQuantity()
        {
            if (State != MachineState.EnterQuantity)
            {
                return;
            }

            AppendMessage(string.Format(" {0}", Input));

            EnsureTransaction();

            int quantity = int.Parse(Input);
            if (quantity > Transaction.Product.Quantity)
            {
                AddMessage("Error: Not Enough Stock!");
                SwitchState(MachineState.EnterQuantity);
            }
            else
            {
                Transaction.Quantity = quantity;
                AddMessage(string.Format("Amount Due: {0}", Transaction.AmountDue));
                SwitchState(MachineState.SelectPaymentMethod);
            }
        }

        #endregion

        #region Transaction

        private void EnsureTransaction()
        {
            if (Transaction == null)
            {
                Transaction = new Transaction();
            }
        }

        #endregion

        #region Payment Method

        private void SelectPaymentMethod()
        {
            if (State != MachineState.SelectPaymentMethod)
            {
                return;
            }

            AppendMessage(string.Format(" {0}", Input));

            EnsureTransaction();

            int choice = int.Parse(Input);
            switch (choice)
            {
                case 1:  // Cash
                    Transaction.SelectPaymentMethod(PaymentMethod.Cash);
                    SwitchState(MachineState.CollectCash);
                    break;
                case 2:  // Credit Card
                    Transaction.SelectPaymentMethod(PaymentMethod.CreditCard);
                    SwitchState(MachineState.EnterCreditCardNumber);
                    break;
                default:
                    AddMessage("Error: Wrong Payment Method!");
                    SwitchState(MachineState.SelectPaymentMethod);
                    break;
            }
        }

        #endregion

        #region Cash

        public void CollectCash(IList<CashSet> cashSets)
        {
            if (State != MachineState.CollectCash)
            {
                return;
            }

            CashReceived.CashSets = cashSets;
            if (CashReceived.Amount < Transaction.AmountDue)
            {
                AddMessage("Error: Not Enough Cash!");
                SwitchState(MachineState.CollectCash);
            }
            else
            {
                AddMessage(string.Format("Received: {0}", CashReceived.Amount));

                EnsureTransaction();

                // calculate change
                CashPayment payment = new CashPayment(CashInHand, CashReceived);
                Transaction.Payment = payment;

                payment.CalculateTotalAmountDue(Transaction.AmountDue);
                payment.CalculateChange();

                CashChange = payment.CashChange;
                AddMessage(string.Format("Change: {0}", CashChange.Amount));

                SwitchState(MachineState.ConfirmTransaction);
            }
        }

        #endregion

        #region Credit Card

        private void EnterCreditCard()
        {
            if (State != MachineState.EnterCreditCardNumber)
            {
                return;
            }

            AppendMessage(string.Format(" {0}", Input));

            EnsureTransaction();

            Transaction.Payment = new CreditCardPayment(Input);
            if (!Transaction.Validate())
            {
                AddMessage("Error: Invalid Credit Card!");
                SwitchState(MachineState.EnterCreditCardNumber);
            }
            else
            {
                AddMessage(string.Format("You will be charged {0}", Transaction.TotalAmountDue));
                SwitchState(MachineState.ConfirmTransaction);
            }
        }

        #endregion

        #region Confirm Transaction

        private void ConfirmTransaction()
        {
            if (State != MachineState.ConfirmTransaction)
            {
                return;
            }

            EnsureTransaction();
            Transaction.Confirm();

            AddMessage(string.Format("Total amount of {0} paid.", Transaction.TotalAmountDue));
            if(Transaction.PaymentMetthod == PaymentMethod.Cash)
            {
                AddMessage("Dispensing Good(s) and Change ... Please collect.");
            }
            else
            {
                AddMessage("Dispensing Good(s) ... Please collect.");
            }
            AddMessage("");
            SwitchState(MachineState.Dispense);
        }

        #endregion
    }
}
