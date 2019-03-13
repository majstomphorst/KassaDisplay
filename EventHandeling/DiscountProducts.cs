using System;

namespace EventHandeling
{
    public class DiscountProducts : Product
    {
        public decimal Discount { get { return -1m * Amount; } }     
        public DiscountProducts(IProduct product) 
                                : base(product.Barcode, 
                                       "DISCOUNT ON" + product.Description, 
                                       ((product.Amount * 3) / 100 * 25) * -1)
        {
        } 
    }

}