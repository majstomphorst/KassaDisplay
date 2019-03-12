using System;
namespace EventHandeling
{
    public class Product : IProduct
    {
        public string Barcode { get; private set; }
        public string Description { get; private set; }
        public decimal Amount { get; private set; }

        public Product(string barcode, string description, decimal amount)
        {
            Barcode = barcode;
            Description = description;
            Amount = amount;
        }

        public string ToFormattedString()
        {
            return String.Format("{0,4} {1,-29} {2:c}", Barcode, Description, Amount);
        }

    }
}
