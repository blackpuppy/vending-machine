using System;
using System.Collections.Generic;
using System.Text;

namespace Acme.VendingMachine.Model
{
    public enum MachineState
    {
        SelectProduct,
        EnterQuantity,
        SelectPaymentMethod,
        EnterCash,
        EnterCreditCardNumber,
        ConfirmTransaction,
        ReturnChange,
    }
}
