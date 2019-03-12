using System;
using System.Collections.Generic;
using EventHandeling;

namespace EventHandelingTest
{

    public class ProductCatalogus
    {
        internal class TestProductCatalogus : IProductCatalogus
        {
            public Product ReturnProduct { get; set; } = null;
            public List<IProduct> ReturnProductList { get; set; } = new List<IProduct>();


            public IProduct FindProductForBarcode(string barcode)
            {
                return ReturnProduct;
            }

            public IList<IProduct> GetAllProducts()
            {
                return ReturnProductList;
            }
        }
    }
}