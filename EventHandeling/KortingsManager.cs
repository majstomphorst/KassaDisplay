using System;
using System.Collections.Generic;
using System.Linq;

namespace EventHandeling
{
    public class KortingsManager
    {
        private DiscountCheck DiscountCheck {get; set;}
        private List<Tuple<int, decimal>> DiscountRules {get; set;}
        private List<IProduct> Cart {get; set;}
        public event EventHandler<BarcodeEventArgs> DiscountAProduct;
        public delegate void DiscountProductsFound(object source, ProductsEventArgs e); 
        
        public KortingsManager()
        {
            Cart = new List<IProduct>();
            DiscountRules = new List<Tuple< int, decimal>>
            {
                Tuple.Create(3, 0.25m)
            };
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
                DiscountCheck = new DiscountCheck(discountRule.Item1, discountRule.Item2);

                var ItemsToRemoveFromCart = DiscountCheck.CheckForDiscount(Cart);

                if (ItemsToRemoveFromCart != null) {
                    RaiseDiscountAProduct(DiscountCheck.DiscountProduct);

                    foreach (var item in ItemsToRemoveFromCart)
                    {
                        Cart.Remove(item);
                    }
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