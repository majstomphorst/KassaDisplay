using System.Collections.Generic;
using System.Linq;

namespace EventHandeling
{
    public class TwoProductDiscount : QuantityThreeDiscount
    {
        private List<string> Barcodes { get; set; }
        private decimal StaticDiscount{ get; set; }
    
        public TwoProductDiscount(List<string> barcodes,decimal staticDiscount)
        {
            Barcodes = barcodes;
            StaticDiscount = staticDiscount;
        }

        public override List<IProduct> CheckForDiscount(List<IProduct> cart)
        {
            // filter Cart only get type(Product) and group the products by barcode
            var posibleProductsForDiscount = cart.Where(product =>  product.GetType() == typeof(Product))
                                     .Where(product => Barcodes.Contains(product.Barcode));
            
            var amountOfGroups = posibleProductsForDiscount.GroupBy(group => group.Barcode).Count();

            if (posibleProductsForDiscount.Count() == 2 && amountOfGroups == 2) {
                
                string description = "Discount";
                decimal cartPrice = 0m;

                foreach (var product in posibleProductsForDiscount)
                {
                    cartPrice += product.Amount;
                    description += "-" + product.Barcode;
                }
                
                DiscountProduct = new DiscountProduct((cartPrice - StaticDiscount) * -1, description);

                return posibleProductsForDiscount.ToList();            
            }

            return null;
        }

    }
    
}