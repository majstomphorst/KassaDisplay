using System;
using System.Collections.Generic;

namespace EventHandeling
{
    public class ProductsEventArgs : EventArgs
    {
        public IList<IProduct> Products {get; set;}

        public void DisplayAllProducts(object source, ProductsEventArgs e)
        {
            Products = e.Products;
        }
    }
}