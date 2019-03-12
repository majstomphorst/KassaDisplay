using System;
using System.Collections.Generic;
using System.Linq;

namespace EventHandeling
{
    public class KortingsManager
    {
        public void RaiseBarcodeScaned(object source, BarcodeEventArgs e)
        {
            System.Console.WriteLine(e.Product.ToFormattedString());
            System.Console.WriteLine("yo");
        }
    }
}