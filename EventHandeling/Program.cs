﻿using System.Collections;


namespace EventHandeling
{
    class Program
    {
        static void Main(string[] args) {

            System.Console.WriteLine("");
            var productCatalogus = new ProductCatalogus();
            Kassa kassa = new Kassa(productCatalogus);

            var console = new ConsoleKassaInterface(kassa);
            var magazijn = new Magazijn();
            var printer = new Printer();

            // subsribe to the kassa
            kassa.DisplayToClient += console.HandleClientDisplay;
            kassa.DisplayAllProducts += console.HandleDisplayAllProducts;
            kassa.BarcodeScanned += magazijn.RaiseBarcodeScaned;
            kassa.PaymentMade += printer.RaisePayment;

            console.Run();
        }
    }
}
