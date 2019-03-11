using System;

namespace EventHandeling
{
    public class BarcodeEventArgs : EventArgs 
    {
        public string Barcode { get; }
        public BarcodeEventArgs(string barcode)
        {
            Barcode = barcode;
        }

        
    }
}