﻿using Acme.VendingMachine.DataAccess;
using Acme.VendingMachine.Model;

namespace Acme.VendingMachine.BusinessLogic
{
    class CashBll
    {
        private readonly CashDal _dal = null;

        public CashBll()
        {
            _dal = new CashDal();
        }

        public CashRegister GetAllCash()
        {
            return _dal.GetAllCash();
        }
    }
}
