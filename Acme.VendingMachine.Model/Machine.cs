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
            CashInHand = CashInHand;

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
                    _messages = new List<string>();
                    _messages.Add("Please select a product:");
                    ClearProductSelected();
                    break;
                case MachineState.EnterQuantity:
                    AddMessage("Please enter quantity:");
                    break;
                case MachineState.SelectPaymentMethod:
                    AddMessage("Please select a payment method (1-Cash 2-Credit Card):");
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
            }
        }

        public void Cancel()
        {
            SwitchState(MachineState.SelectProduct);
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
                List<string> messagesToDisplay = _messages.TakeLast(3).ToList();
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
                AddMessage("Please select a product:");
                ClearInput();
            }
            else if (selected.Quantity == 0)
            {
                AddMessage("Error: Product Sold Out!");
                AddMessage("Please select a product:");
                ClearInput();
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
                ClearInput();
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
            if (choice < 1 || choice > 2)
            {
                AddMessage("Error: Wrong Payment Method!");
                SwitchState(MachineState.SelectPaymentMethod);
            }
            else
            {
                Transaction.Quantity = choice;
                AddMessage(string.Format("Amount Due: {0}", Transaction.AmountDue));
                SwitchState(MachineState.SelectPaymentMethod);
            }
        }

        #endregion
    }
}
