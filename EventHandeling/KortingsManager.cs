using System;
using System.Collections.Generic;
using System.Linq;

namespace EventHandeling
{
    public class KortingsManager
    {
        private List<IProduct> Cart {get; set;} = new List<IProduct>();
        private List<DiscountProducts> DiscountProducts = new List<DiscountProducts>();
        public event EventHandler<BarcodeEventArgs> DiscountAProduct;
        public delegate void DiscountProductsFound(object source, ProductsEventArgs e); 
        public void RaiseBarcodeScaned(object source, BarcodeEventArgs e)
        {
            Cart.Add(e.Product);
            checkCartForDiscount();
        }

        private void checkCartForDiscount() 
        {
            var discountProductGroup = Cart.Where(product =>  product.GetType() == typeof(Product))
                                            .GroupBy(product => product.Barcode)
                                            .Where(group => group.Count() >= 3)
                                            .FirstOrDefault();

            if (discountProductGroup != null) {
                var product = discountProductGroup.First();
                var discountProduct = new DiscountProducts(product);
                DiscountProducts.Add(discountProduct);

                Cart.RemoveAll(p => p.Barcode == product.Barcode);
                RaiseDiscountAProduct(discountProduct);

            }
        }
        protected virtual void RaiseDiscountAProduct(IProduct product) 
        {
            if (DiscountAProduct != null)
            {
                DiscountAProduct(this, new BarcodeEventArgs(product));
            }
        }
    }
}