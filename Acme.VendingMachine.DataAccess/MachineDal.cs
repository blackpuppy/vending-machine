using Acme.VendingMachine.Model;
using System.Collections.Generic;

namespace Acme.VendingMachine.DataAccess
{
    public class MachineDal
    {
        private ProductDal _prodductDal;

        #region Mock cash register

        static Machine _machine;

        static MachineDal()
        {
            ProductDal prodductDal = new ProductDal();
            CashDal cashDal = new CashDal();

            IList<Product> products = prodductDal.GetAllProducts();
            CashRegister cashInHand = cashDal.GetAllCash();

            _machine = new Machine(products, cashInHand);
        }

        #endregion

        public MachineDal(ProductDal prodductDal)
        {
            _prodductDal = prodductDal;
        }

        public Machine Get()
        {
            return _machine;
        }
    }
}
