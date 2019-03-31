using System;

namespace Acme.VendingMachine.Model
{
    public class Product
    {
        public int ItemNo { get; set; }
        public int Price { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }

        public bool Seleted { get; set; }

        public bool SoldOut => Quantity == 0;
        public string DisplayQuantity => (Quantity == 0 ? "Sold Out" : Quantity.ToString());
    }
}
