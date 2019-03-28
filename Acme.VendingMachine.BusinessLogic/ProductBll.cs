using Acme.VendingMachine.DataAccess;
using Acme.VendingMachine.Model;
using System;
using System.Collections.Generic;

namespace Acme.VendingMachine.BusinessLogic
{
    public class ProductBll
    {
        private readonly ProductDal _dal = null;

        public ProductBll()
        {
            _dal = new ProductDal();
        }

        public IList<Product> GetAllProducts()
        {
            return _dal.GetAllProducts();
        }
    }
}
