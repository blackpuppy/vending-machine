using System;

namespace Acme.VendingMachine.Model
{
    public class Product
    {
        public int ItemNo { get; set; }
        public int Price { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }

        public bool SoldOut => this.Quantity == 0;

        public bool Seleted { get; set; }
    }
}
