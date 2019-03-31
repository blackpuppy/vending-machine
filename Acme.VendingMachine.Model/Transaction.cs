namespace Acme.VendingMachine.Model
{
    public class Transaction
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }

        public int AmountDue
        {
            get
            {
                int due = 0;

                if (Product!=null && Quantity > 0 && Quantity <= Product.Quantity)
                {
                    due = Product.Price * Quantity;
                }

                return due;
            }
        }
    }
}
