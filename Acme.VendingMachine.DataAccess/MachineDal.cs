using Acme.VendingMachine.Model;

namespace Acme.VendingMachine.DataAccess
{
    public class MachineDal
    {
        #region Mock cash register

        static Machine _machine;

        static MachineDal()
        {
            ProductDal prodductDal = new ProductDal();
            CashDal cashDal = new CashDal();

            _machine = new Machine();
            _machine.Products = prodductDal.GetAllProducts();
            _machine.CashInHand = cashDal.GetAllCash();
        }

        #endregion

        private ProductDal _prodductDal;

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
