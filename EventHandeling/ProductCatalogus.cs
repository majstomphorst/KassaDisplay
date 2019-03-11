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
                new Product("01", "Buy this produc: It is sick!", 1m),
                new Product("02", "I do not want to go away", 2m),
                new Product("03", "I'm a robot.", 3m)
            };
        }

        public ProductCatalogus(List<IProduct> products)
        {
            Products = new List<IProduct>();
            foreach (var product in products)
            {
                Products.Add(product);
            }
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
