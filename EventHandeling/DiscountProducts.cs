using System;

namespace EventHandeling
{
    public class DiscountProduct : Product
    {
        public decimal Discount { get { return -1m * Amount; } }
        public DiscountProduct(IProduct product,decimal discount) 
                                : base(product.Barcode, 
                                       "DISCOUNT ON" + product.Description, 
                                       discount)
        {
        }
        public DiscountProduct(decimal discount) 
                                : base("D-", 
                                  "DISCOUNT", 
                                  discount)
        {
        }

    
    }

}
