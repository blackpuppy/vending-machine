using System.Collections.Generic;
using Acme.VendingMachine.Model;

namespace Acme.VendingMachine.BusinessLogic
{
    public interface IProductBll
    {
        IList<Product> GetAllProducts();
    }
}