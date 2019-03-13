using System;

namespace EventHandeling
{
    public class DiscountProducts : Product
    {
        private Decimal Discount { get; set; } = 0m;
            
        public DiscountProducts(IProduct product) 
                                : base(product.Barcode, product.Description, product.Amount)
        {
            Discount = ((product.Amount * 3) / 100 * 25) * -1;
        }

        public IProduct getDiscountedProductForKassaCart()
        {
            var descriptionWithDiscountText = Description + "---YOU GOT DISCOUNT <3";
            return new Product(Barcode,descriptionWithDiscountText,Discount);
        }

    }

}