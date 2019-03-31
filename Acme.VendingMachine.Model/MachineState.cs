using System;
using System.Collections.Generic;
using System.Text;

namespace Acme.VendingMachine.Model
{
    public enum MachineState
    {
        SelectProduct,
        EnterQuantity,
        MakePayment,
        ReturnChange,
    }
}
