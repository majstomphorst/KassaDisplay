using System;
using System.Collections.Generic;

namespace EventHandeling
{
    public class RaiseDisplayAllProductsEventARgs
    {
        public IList<IProduct> Products {get; set;}

        public void DisplayAllProducts(object source, RaiseDisplayAllProductsEventARgs e)
        {
            Products = e.Products;
        }
    }
}