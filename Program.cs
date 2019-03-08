using System;
using System.Collections;
using System.Collections.Generic;

namespace EventHandeling
{
    class Program
    {
        static void Main(string[] args) {

            ProductCatalogus productCatalogus = new ProductCatalogus();
            IKassa kassa = new Kassa(productCatalogus);
            var console = new ConsoleKassaInterface(kassa);

            console.Run();
        }
    }
}
