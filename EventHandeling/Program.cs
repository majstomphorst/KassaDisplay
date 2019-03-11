using System.Collections;

namespace EventHandeling
{
    class Program
    {
        static void Main(string[] args) {
            var productCatalogus = new ProductCatalogus();
            Kassa kassa = new Kassa(productCatalogus); // publisher 
            var console = new ConsoleKassaInterface(kassa);

            var magazijn = new Magazijn(); // subscriber

            // subsribe to the kassa
            kassa.BarcodeScanded += magazijn.OnBarcodeScaned;

            console.Run();
        }
    }
}
