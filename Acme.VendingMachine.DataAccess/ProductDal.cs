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
                //new Product() { ItemNo = 101, Price = 109, Name = "Orange Pie (150g)", Quantity = 5 },
                new Product() { ItemNo = 104, Price = 189, Name = "Chocalete Bar (220g)", Quantity = 5 },
                //new Product() { ItemNo = 105, Price = 219, Name = "Crackers (278g)", Quantity = 5 },
                new Product() { ItemNo = 204, Price = 349, Name = "Hungry Burger", Quantity = 5 },
                new Product() { ItemNo = 302, Price = 569, Name = "Taste Beijing duck tomato", Quantity = 5 },
                new Product() { ItemNo = 307, Price = 429, Name = "The color of the cook", Quantity = 5 },
                new Product() { ItemNo = 316, Price = 589, Name = "Drunken steak", Quantity = 5 },
                new Product() { ItemNo = 325, Price = 469, Name = "Sweat and sour carp", Quantity = 5 },
                new Product() { ItemNo = 333, Price = 279, Name = "Cucumber with egg", Quantity = 5 },
                new Product() { ItemNo = 706, Price = 459, Name = "Snow fungus soup", Quantity = 5 },
                new Product() { ItemNo = 801, Price = 119, Name = "Red Tea", Quantity = 5 },
                new Product() { ItemNo = 805, Price = 129, Name = "Milk Tea", Quantity = 5 },
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
