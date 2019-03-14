using System;

namespace EventHandeling
{
    public class DiscountProduct : Product
    {
        public decimal Discount { get { return -1m * Amount; } }
        public DiscountProduct(IProduct product,int n,decimal percentage) 
                                : base(product.Barcode, 
                                       "DISCOUNT ON" + product.Description, 
                                       ((product.Amount * n) * percentage * -1))
        {
        } 
    }

}
