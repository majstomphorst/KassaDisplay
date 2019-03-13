using System.Collections.Generic;
using System.Linq;

namespace EventHandeling
{
    public class KortingsManager
    {
        private List<IProduct> Cart {get; set;} = new List<IProduct>();
        private List<DiscountProducts> DiscountProducts = new List<DiscountProducts>();
        public void RaiseBarcodeScaned(object source, BarcodeEventArgs e)
        {
            Cart.Add(e.Product);
            checkCartForDiscount();
        }

        private void checkCartForDiscount() 
        {
            var discountProducts = Cart.GroupBy(product => product.Barcode).Where(group => group.Count() >= 3).ToList();

            if (discountProducts.Count >= 1) {
                DiscountProducts.Add(new DiscountProducts(discountProducts[0].First()));
                Cart.RemoveAll(product => product.Barcode == discountProducts[0].First().Barcode);
                
            }
        }
    }
}