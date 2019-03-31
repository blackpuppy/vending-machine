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

        public IList<string> Messages { get; set; }

        public string DisplayMessage
        {
            get
            {
                return string.Join(Environment.NewLine, this.Messages);
            }
        }

        public Machine()
        {
            this.Messages = new List<string>();
            this.Messages.Add("this is a test");
            this.Messages.Add("message");
        }
    }
}
