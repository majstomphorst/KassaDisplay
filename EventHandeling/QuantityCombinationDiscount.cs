using System.Collections.Generic;
using System.Linq;

namespace EventHandeling
{
    public class QuantityCombinationDiscount : IDiscountCheck
    {
        public DiscountProduct DiscountProduct { get; private set;}
        public bool ContinueAfterDiscount {get;} = true;
        private int N { get; set; }
        private decimal Percentage { get; set; }
        private List<string> Barcodes { get; set; }
    
        public QuantityCombinationDiscount(List<string> barcodes, int n, decimal percentage)
        {
            if (barcodes.Count == 2 && n > 0 && percentage > 0 )
            {
                Barcodes = barcodes;
                N = n;
                Percentage = percentage;
            }
        }

        public List<IProduct> CheckForDiscount(List<IProduct> cart)
        {
            // filter Cart only get type(Product) and group the products by barcode
            var posibleProductsForDiscount = cart.Where(product =>  product.GetType() == typeof(Product))
                                     .Where(product => Barcodes.Contains(product.Barcode));
            

            if (posibleProductsForDiscount.Count() == N) {
                System.Console.WriteLine("DISCOUNT!");
                
                decimal cartPrice = 0m;
                foreach (var product in posibleProductsForDiscount)
                {
                    cartPrice += product.Amount;
                }

                var discount = cartPrice * Percentage * -1;
                DiscountProduct = new DiscountProduct(discount);

                return posibleProductsForDiscount.ToList();            
            }

            return null;
        }

    }
    
}