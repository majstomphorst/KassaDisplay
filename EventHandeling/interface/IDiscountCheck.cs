using System;
using System.Collections.Generic;

namespace EventHandeling
{
    public interface IDiscountCheck
    {
        /// <summary>
        /// The amount of discount that to be given
        /// </summary>
        DiscountProduct DiscountProduct{ get;}

        bool ContinueAfterDiscount {get;}

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
    
}