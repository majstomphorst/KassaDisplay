using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;


namespace EventHandeling
{
    class Program
    {
        static void Main(string[] args) {
            var productCatalogus = new ProductCatalogus();
            Kassa kassa = new Kassa(productCatalogus); // publisher 
            var console = new ConsoleKassaInterface(kassa);
            var magazijn = new Magazijn(); // subscriber
            var printer = new Printer(); // subscriber
            // subsribe to the kassa
            kassa.BarcodeScanded += magazijn.RaiseBarcodeScaned;
            kassa.PaymentMade += printer.RaisePayment;

            console.Run();
        }
    }

    class Printer 
    {
        public void RaisePayment(object sourcre, PaymentMadeEventArgs e)
        {
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            var SortedCart = SortCartByProductBarcode(e.Cart);
            foreach (var product in SortedCart )
            {
                System.Console.WriteLine(product.ToString());
            }
            Console.ResetColor();
        }

        public List<IProduct> SortCartByProductBarcode(List<IProduct> UnSortedCart) {
            return UnSortedCart.OrderBy(product => product.Barcode).ToList();
        }
    }
}
