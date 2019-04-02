using System.Globalization;

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

        public string DisplayPrice
        {
            get
            {
                //var ri = new RegionInfo(System.Threading.Thread.CurrentThread.CurrentUICulture.LCID);
                NumberFormatInfo LocalFormat = (NumberFormatInfo)NumberFormatInfo.CurrentInfo.Clone();
                //LocalFormat.CurrencySymbol = ri.ISOCurrencySymbol;
                LocalFormat.CurrencySymbol = "$";
                return (Price / 100.0).ToString("c", LocalFormat);
            }
        }
    }
}
