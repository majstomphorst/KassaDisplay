using System;
using System.Linq;
using System.Collections.Generic;


namespace EventHandeling
{
    class Printer 
    {
        public void RaisePayment(object sourcre, PaymentMadeEventArgs e)
        {
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            var SortedCart = SortCartByProductBarcode(e.Cart);
            System.Console.WriteLine();
            foreach (var product in SortedCart )
            {
                System.Console.WriteLine(product.ToString());
            }
            System.Console.WriteLine();
            Console.ResetColor();
        }

        public List<IProduct> SortCartByProductBarcode(List<IProduct> UnSortedCart) {
            return UnSortedCart.OrderBy(product => product.Barcode).ToList();
        }
    }
}
