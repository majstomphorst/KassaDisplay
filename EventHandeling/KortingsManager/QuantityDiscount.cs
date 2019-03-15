using System.Collections.Generic;
using System.Linq;

namespace EventHandeling
{
    public class QuantityDiscount : IDiscountCheck
    {
        public DiscountProduct DiscountProduct { get; private set;}
        public bool ContinueAfterDiscount {get;} = true;
        private int N { get; set; }
        private decimal Percentage { get; set; }
    
        public QuantityDiscount(int n, decimal percentage)
        {
            if (n > 0 && percentage > 0 )
            {
                N = n;
                Percentage = percentage;
            }
        }

        public List<IProduct> CheckForDiscount(List<IProduct> cart)
        {
            // filter Cart only get type(Product) and group the products by barcode
            var productGroupBarcode = cart.Where(product =>  product.GetType() == typeof(Product))
                                          .GroupBy(product => product.Barcode);

            // get one item if N or more are the same product
            var discountProductGroup = productGroupBarcode.Where(group => group.Count() >= N)
                                                          .FirstOrDefault();

            if (discountProductGroup != null) {
                var product = discountProductGroup.First();

                decimal discount = ((product.Amount * N) * Percentage) * -1;
                DiscountProduct = new DiscountProduct(product, discount);

                return discountProductGroup.ToList();                
            }

            return null;
        }

    }
    
}