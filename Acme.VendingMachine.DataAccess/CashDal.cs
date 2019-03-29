using Acme.VendingMachine.Model;
using System.Collections.Generic;

namespace Acme.VendingMachine.DataAccess
{
    public class CashDal
    {
        #region Mock cash register

        static CashRegister _cashRegister;

        static CashDal()
        {
            _cashRegister = new CashRegister();
            _cashRegister.CashSets = new List<CashSet>(new CashSet[] {
                new CashSet() { Denomination = CashDenomination.NICKEL, Quantity=5 },
                new CashSet() { Denomination = CashDenomination.DIME, Quantity=5 },
                new CashSet() { Denomination = CashDenomination.QUARTER, Quantity=5 },
                new CashSet() { Denomination = CashDenomination.DOLLAR, Quantity=5 },
                new CashSet() { Denomination = CashDenomination.FIVE_DOLLAR, Quantity=5 },
            });
        }

        #endregion

        #region CRUD

        public CashRegister GetAllCash()
        {
            return _cashRegister;
        }

        #endregion

    }
}
