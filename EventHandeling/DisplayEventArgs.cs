using System;

namespace EventHandeling
{
    public class DisplayEventArgs : EventArgs 
    {
        public decimal TotalPrice { get; }
        public IProduct Product { get; }
        public string ProductInfo { get { return Product.ToFormattedString(); } }

        public DisplayEventArgs(decimal totalPrice, IProduct product)
        {
           TotalPrice = totalPrice;
           Product = product;
        }
    }
}