using System;

namespace EventHandeling
{
    public class DiscountProducts : Product
    {
        public decimal Discount { get { return -1m * Amount; } }     
        public DiscountProducts(IProduct product) 
                                : base(product.Barcode, 
                                       product.Description + "---YOU GOT DISCOUNT <3", 
                                       ((product.Amount * 3) / 100 * 25) * -1)
        {
        } 
    }

}