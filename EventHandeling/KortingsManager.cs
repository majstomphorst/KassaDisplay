using System;
using System.Collections.Generic;
using System.Linq;

namespace EventHandeling
{
    public class KortingsManager
    {
        private List<IProduct> Cart {get; set;}
        public event EventHandler<BarcodeEventArgs> DiscountAProduct;
        public delegate void DiscountProductsFound(object source, ProductsEventArgs e); 
        
        public KortingsManager()
        {
            Cart = new List<IProduct>();
        }
        
        public void RaiseBarcodeScaned(object source, BarcodeEventArgs e)
        {
            Cart.Add(e.Product);
            checkCartForDiscount();
        }

        private void checkCartForDiscount() 
        {
            // filter Cart only get type(Product) and group the products by barcode
            var productGroupBarcode = Cart.Where(product =>  product.GetType() == typeof(Product))
                                          .GroupBy(product => product.Barcode);

            // get one item if 3 or more are the same product
            var discountProductGroup = productGroupBarcode.Where(group => group.Count() >= 3)
                                                          .FirstOrDefault();

            if (discountProductGroup != null) {
                var product = discountProductGroup.First();
                var discountProduct = new DiscountProducts(product);
                Cart.Add(discountProduct);

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
        public void RaisePayment(object sourcre, PaymentMadeEventArgs e)
        {
            Cart = new List<IProduct>();
        }

        

    }
}