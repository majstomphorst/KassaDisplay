using System.Collections.Generic;
using System.Linq;

namespace EventHandeling
{
    public class CombinationDiscountOfProduct : QuantityThreeDiscount
    {
        private List<string> Barcodes { get; set; }
        
        public CombinationDiscountOfProduct(List<string> barcodes, int n)
                                            :base(n) 
        {
            // TODO: check for valid values
            Barcodes = barcodes;
        }

        public override List<IProduct> CheckForDiscount(List<IProduct> cart)
        {
            // filter Cart only get type(Product) and group the products by barcode
            var posibleProductsForDiscount = cart.Where(product =>  product.GetType() == typeof(Product))
                                     .Where(product => Barcodes.Contains(product.Barcode));
            
            var amountOfGroups = posibleProductsForDiscount.GroupBy(group => group.Barcode).Count();

            if (posibleProductsForDiscount.Count() == N && amountOfGroups >= 2) {
                
                string description = "discount-cheapest-";
                decimal cartPrice = 0m;
                decimal discount = posibleProductsForDiscount.First().Amount;
                foreach (var product in posibleProductsForDiscount)
                {
                    if (product.Amount < discount) {
                        discount = product.Amount;
                    }
                    cartPrice += product.Amount;
                    description += product.Barcode + "-";
                }
                
                DiscountProduct = new DiscountProduct(discount * -1,description);

                return posibleProductsForDiscount.ToList();            
            }

            return null;
        }

    }
    
}