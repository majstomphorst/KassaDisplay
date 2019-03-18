using System;
using System.Collections.Generic;
using System.Linq;

namespace EventHandeling
{
    public class KortingsManager
    {
        private List<IDiscountCheck> DiscountRules {get; } = new List<IDiscountCheck>();
        private List<IProduct> Cart {get; set;}
        public event EventHandler<BarcodeEventArgs> DiscountAProduct;
        public delegate void DiscountProductsFound(object source, ProductsEventArgs e); 
        
        public KortingsManager()
        {
            Cart = new List<IProduct>();
        }
        public void QuantityThreeDiscount() {
            DiscountRules.Add(new QuantityThreeDiscount());
        }
        public void CombinationDiscount(List<string> barcodes, int n, decimal percentage) {
            DiscountRules.Add(new CombinationDiscount(barcodes,  n,  percentage));
        }
        public void CombinationDiscountOfProduct(List<string> barcodes, int n) {
            DiscountRules.Add(new CombinationDiscountOfProduct(barcodes, n));
        }
        public void TwoProductDiscount(List<string> barcodes, decimal staticDiscount) {
            DiscountRules.Add(new TwoProductDiscount(barcodes,staticDiscount));
        }


        public void RaiseBarcodeScaned(object source, BarcodeEventArgs e)
        {
            Cart.Add(e.Product);
            checkCartForDiscount();
        }

        private void checkCartForDiscount() 
        {  
            foreach (var discountRule in DiscountRules)
            {
                var itemsToRemoveFromCart = discountRule.CheckForDiscount(Cart);
                if (itemsToRemoveFromCart != null) {
                    RaiseDiscountAProduct(discountRule.DiscountProduct);
                    itemsToRemoveFromCart.Select(product => Cart.Remove(product)).ToList();

                    if (!discountRule.ContinueAfterDiscount) { return; }
                }
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