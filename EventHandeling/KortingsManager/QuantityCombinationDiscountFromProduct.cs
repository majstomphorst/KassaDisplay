using System.Collections.Generic;
using System.Linq;

namespace EventHandeling
{
    public class QuantityCombinationDiscountFromProduct : IDiscountCheck
    {
        public DiscountProduct DiscountProduct { get; private set;}
        public bool ContinueAfterDiscount {get;} = true;
        private int N { get; set; }
        private decimal Percentage { get; set; }
        private List<string> Barcodes { get; set; }
    
        public QuantityCombinationDiscountFromProduct(List<string> barcodes, int n, decimal percentage)
        {
            // TODO: check for valid values
            Barcodes = barcodes;
            N = n;
            Percentage = percentage;
        }

        public List<IProduct> CheckForDiscount(List<IProduct> cart)
        {
            // filter Cart only get type(Product) and group the products by barcode
            var posibleProductsForDiscount = cart.Where(product =>  product.GetType() == typeof(Product))
                                     .Where(product => Barcodes.Contains(product.Barcode));
            
            var amountOfGroups = posibleProductsForDiscount.GroupBy(group => group.Barcode).Count();

            if (posibleProductsForDiscount.Count() == N && amountOfGroups == 2) {
                
                string description = "discount-A+B-0,45c-";
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