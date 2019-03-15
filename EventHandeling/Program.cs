using System.Collections;
using System.Collections.Generic;


namespace EventHandeling
{
    class Program
    {
        static void Main(string[] args) {

            System.Console.WriteLine("");
            var productCatalogus = new ProductCatalogus();
            Kassa kassa = new Kassa(productCatalogus);

            var console = new ConsoleKassaInterface(kassa);
            var manager = new KortingsManager();
            // manager.addQuantityDiscountRule(5,0.5m);
            var barcodes = new List<string>();
            barcodes.Add("01");
            barcodes.Add("02");
            manager.addQuantityCombinationDiscount(barcodes,3,0.2m);

            var magazijn = new Magazijn();
            var printer = new Printer();

            // subsribe to the kassa
            kassa.DisplayToClient += console.HandleClientDisplay;
            kassa.DisplayAllProducts += console.HandleDisplayAllProducts;
            kassa.BarcodeScanned += magazijn.RaiseBarcodeScaned;
            kassa.BarcodeScanned += manager.RaiseBarcodeScaned;
            kassa.PaymentMade += printer.RaisePayment;
            kassa.PaymentMade += manager.RaisePayment;

            manager.DiscountAProduct += kassa.RaiseDiscountProduct;

            console.Run();
        }
    }
}
