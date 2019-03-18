using System.Collections.Generic;
using System.Linq;

namespace EventHandeling
{
    public class QuantityThreeDiscount : IDiscountCheck
    {
        public DiscountProduct DiscountProduct { get; set;}
        public bool ContinueAfterDiscount {get;} = true;
        protected int N { get; set; }
        protected decimal Percentage { get; set; }
    
        public QuantityThreeDiscount(int n = 3, decimal percentage = 0.25m)
        {
            N = n;
            Percentage = percentage;
        }

        public virtual List<IProduct> CheckForDiscount(List<IProduct> cart)
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