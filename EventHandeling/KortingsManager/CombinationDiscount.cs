using System.Collections.Generic;
using System.Linq;

namespace EventHandeling
{
    public class CombinationDiscount : QuantityThreeDiscount
    {
        private List<string> Barcodes { get; set; }

        public CombinationDiscount (List<string> barcodes,int n, decimal percentage)
                             :base (n, percentage) 
        {
            Barcodes = barcodes;
        }

        public override List<IProduct> CheckForDiscount(List<IProduct> cart)
        {
            // filter Cart only get type(Product) and group the products by barcode
            var posibleProductsForDiscount = cart.Where(product =>  product.GetType() == typeof(Product))
                                     .Where(product => Barcodes.Contains(product.Barcode));
            

            if (posibleProductsForDiscount.Count() == N) {
                
                string description = "discount-";
                decimal cartPrice = 0m;

                foreach (var product in posibleProductsForDiscount)
                {
                    cartPrice += product.Amount;
                    description += product.Barcode + "-";
                }

                var discount = cartPrice * Percentage * -1;
                DiscountProduct = new DiscountProduct(discount,description);
                
                return posibleProductsForDiscount.ToList();            
            }

            return null;
        }

    }
    
}