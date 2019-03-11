using System;

namespace EventHandeling
{
    public class BarcodeScandedEventArgs : EventArgs 
    {
        public string Barcode { get; set; }
    }
}