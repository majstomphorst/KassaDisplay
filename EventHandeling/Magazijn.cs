using System;
using System.Collections.Generic;

namespace EventHandeling
{
    class Magazijn 
    {
        private List<string> ScanedBarcodes = new List<string>();
        public void OnBarcodeScaned(object source, BarcodeScandedEventArgs e)
        {
            ScanedBarcodes.Add(e.Barcode);
            System.Console.WriteLine("whats up Iventory knows what happend! {0}",e.Barcode);
        }
    }
}
