using System.Collections;


namespace EventHandeling
{
    class Program
    {
        static void Main(string[] args) {
            System.Console.WriteLine("");
            var productCatalogus = new ProductCatalogus();
            Kassa kassa = new Kassa(productCatalogus); // publisher

            var console = new ConsoleKassaInterface(kassa);
            var magazijn = new Magazijn(); // subscriber
            var printer = new Printer(); // subscriber
            // subsribe to the kassa
            kassa.ClientDisplay += console.RaiseClientDisplay;
            kassa.DisplayAllProducts += console.RaiseDisplayAllProducts;
            kassa.BarcodeScanded += magazijn.RaiseBarcodeScaned;
            kassa.PaymentMade += printer.RaisePayment;

            console.Run();
        }
    }
}
