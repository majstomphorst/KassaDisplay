using System;
using System.Collections.Generic;

namespace EventHandeling
{
    class Magazijn 
    {
        private List<string> ScanedBarcodes = new List<string>();
        public void RaiseBarcodeScaned(object source, BarcodeScandedEventArgs e)
        {
            ScanedBarcodes.Add(e.Barcode);
        }
    }
}
