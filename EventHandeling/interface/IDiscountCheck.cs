using System;
using System.Collections.Generic;
using System.Linq;

namespace EventHandeling
{
    public interface IDiscountCheck
    {
        /// <summary>
        /// The amount of discount that to be given
        /// </summary>
        DiscountProduct DiscountProduct{ get;}

        /// <summary>
        /// checks if the cart fulfils the requirements of a discount
        /// </summary>
        /// <param name="Products">a list of products</param>
        /// <retruns>
        /// a list op products that need to be removed from the car
        /// or nill
        /// and it sets the Discount.
        ///</returnes>
        List<IProduct> CheckForDiscount(List<IProduct> products);
    }

    public class DiscountCheck : IDiscountCheck
    {
        public DiscountProduct DiscountProduct { get; private set;}
        private int N { get; set;}
        private decimal Percentage { get; set;}

        public DiscountCheck(int n, decimal percentage)
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
                DiscountProduct = new DiscountProduct(product, N, Percentage);
                return discountProductGroup.ToList();                
            }

            return null;
        }

    }
    
}