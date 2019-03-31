using System;
using System.Collections.Generic;
using System.Text;

namespace Acme.VendingMachine.Model
{
    public class Machine
    {
        public IList<Product> Products { get; set; }

        public CashRegister CashInHand { get; set; }
        public CashRegister CashReceived { get; set; }
        public CashRegister CashChange { get; set; }

        private IList<string> _messages { get; set; }

        public string DisplayMessage
        {
            get
            {
                return string.Join(Environment.NewLine, this._messages);
            }
        }

        public Machine()
        {
            this._messages = new List<string>();
        }

        public void AppendMessage(string message)
        {
            string lastMsg = this._messages[this._messages.Count - 1];
            this._messages[this._messages.Count - 1] = lastMsg + message;
        }

        public void AddMessage(string message)
        {
            this._messages.Add(message);
        }
    }
}
