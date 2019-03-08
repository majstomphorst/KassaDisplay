using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace EventHandeling
{
    public class ProductCatalogus : IProductCatalogus
    {
        private List<IProduct> Products { get; set; }

        public ProductCatalogus()
        {
            Products = new List<IProduct>
            {
                new Product("01", "By tis product its sick!", 1m),
                new Product("02", "I do not want to go!", 2m),
                new Product("03", "I'm a robot.", 3m)
            };
        }

        public IProduct FindProductForBarcode(string barcode)
        {
            return Products.FirstOrDefault(product => product.Barcode.Equals(barcode));
        }

        public IList<IProduct> GetAllProducts()
        {
            return Products;
        }
    }
}
