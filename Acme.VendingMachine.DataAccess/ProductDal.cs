using Acme.VendingMachine.Model;
using System.Collections.Generic;
using System.Linq;

namespace Acme.VendingMachine.DataAccess
{
    public class ProductDal
    {
        #region Mock product storeage

        static List<Product> _products;

        static ProductDal()
        {
            _products = new List<Product>(new Product[] {
                new Product() { ItemNo = 101, Price = 299, Name = "Orange Pie (350g)" },
                new Product() { ItemNo = 104, Price = 389, Name = "Chocalete Bar (250g)" },
                new Product() { ItemNo = 105, Price = 449, Name = "Crackers (460g)" },
                new Product() { ItemNo = 204, Price = 849, Name = "Hungry Burger" },
                new Product() { ItemNo = 302, Price = 1469, Name = "Taste Beijing duck tomato" },
                new Product() { ItemNo = 307, Price = 1529, Name = "The color of the cook" },
                new Product() { ItemNo = 316, Price = 1189, Name = "Drunken steak" },
                new Product() { ItemNo = 325, Price = 1369, Name = "Sweat and sour carp" },
                new Product() { ItemNo = 333, Price = 779, Name = "Cucumber with egg" },
                new Product() { ItemNo = 706, Price = 929, Name = "Snow fungus soup" },
                new Product() { ItemNo = 801, Price = 349, Name = "Red Tea" },
                new Product() { ItemNo = 805, Price = 429, Name = "Milk Tea" },
            });
        }

        #endregion

        #region CRUD

        public IList<Product> GetAllProducts()
        {
            return _products;
        }

        public Product Get(int itemNo)
        {
            return _products.FirstOrDefault(p => p.ItemNo == itemNo);
        }

        #endregion
    }
}
