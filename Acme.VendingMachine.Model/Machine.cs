using System;
using System.Collections.Generic;
using System.Linq;

namespace Acme.VendingMachine.Model
{
    public class Machine
    {
        public Machine()
        {
            this.SwitchState(MachineState.SelectProduct);
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
            this.ClearInput();
            this.State = newState;

            switch (this.State)
            {
                case MachineState.SelectProduct:
                    this._messages = new List<string>();
                    this._messages.Add("Please select a product:");
                    break;
                case MachineState.EnterQuantity:
                    this.AddMessage("Please enter quantity:");
                    break;
            }
        }

        public void Done()
        {
            switch (this.State)
            {
                case MachineState.SelectProduct:
                    this.SelectProduct();
                    break;
            }
        }

        public void Cancel()
        {
            this.SwitchState(MachineState.SelectProduct);
        }

        #endregion

        #region Input

        public string Input { get; set; }

        public void AddInput(string number)
        {
            this.Input += number;
        }

        public void DeleteInput()
        {
            if (!string.IsNullOrEmpty(this.Input))
            {
                this.Input = this.Input.Substring(0, this.Input.Length - 1);
            }
        }

        public void ClearInput()
        {
            this.Input = null;
        }

        #endregion

        #region Message

        private IList<string> _messages { get; set; }

        public string DisplayMessage
        {
            get
            {
                List<string> messagesToDisplay = this._messages.TakeLast(3).ToList();
                string message = string.Join(Environment.NewLine, messagesToDisplay);

                if (!string.IsNullOrEmpty(this.Input))
                {
                    message += " " + this.Input;
                }

                return message;
            }
        }

        public void AppendMessage(string message)
        {
            int count = this._messages.Count;
            if (count > 0)
            {
                string lastMsg = this._messages[this._messages.Count - 1];
                this._messages[this._messages.Count - 1] = lastMsg + message;
            }
            else
            {
                AddMessage(message);
            }
        }

        public void AddMessage(string message)
        {
            this._messages.Add(message);
        }

        #endregion

        #region Select Product

        public void SelectProduct()
        {
            if (this.State != MachineState.SelectProduct)
            {
                return;
            }

            Product selected = this.Products.FirstOrDefault(p => p.ItemNo == int.Parse(this.Input));
            if (selected == null)
            {
                this.AddMessage("Error: Product Not Found!");
                this.AddMessage("Please select a product:");
                this.ClearInput();
            }
            else if (selected.Quantity == 0)
            {
                this.AddMessage("Error: Product Sold Out!");
                this.AddMessage("Please select a product:");
                this.ClearInput();
            }
            else
            {
                this.Products.All(p => p.Seleted = false);
                selected.Seleted = true;
                this.AddMessage(string.Format("# {0} {1} selected!", selected.ItemNo, selected.Name));

                this.SwitchState(MachineState.EnterQuantity);
            }
        }

        #endregion
    }
}
