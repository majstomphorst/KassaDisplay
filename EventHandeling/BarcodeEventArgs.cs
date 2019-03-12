using System;

namespace EventHandeling
{
    public class BarcodeEventArgs : EventArgs 
    {
        public IProduct Product { get; }
        public BarcodeEventArgs(IProduct product)
        {
            Product = product;
        }
    }
}