﻿using System;
using System.Collections.Generic;

namespace EventHandeling
{
    /// <summary>
    /// Class for all interaction with the console for the kassa
    /// </summary>
    class ConsoleKassaInterface : IKassaDisplay
    {
        public IKassa Kassa { get; }

        public ConsoleKassaInterface(IKassa kassa) {
            Kassa = kassa;
            Kassa.setDisplay((IKassaDisplay)this);
            Kassa.showAllProducts();
        }

        public void Run() {
            ShowMenu();
            string input = Console.ReadLine().ToLower().Trim();

            while (input != "q") {                
                switch(input.Substring(0, 1)) {
                    case "p":
                        Kassa.showAllProducts();
                        break;

                    case "b":
                        decimal amount = decimal.Parse(input.Substring(1));
                        var result = Kassa.initatePayment(amount);
                        if (result < 0) {
                            Console.Out.WriteLine("Payment failed");
                        } else if (result == 0) {
                            Console.Out.WriteLine("Payment succesfull");
                        } else {
                            Console.Out.WriteLine("Payment done, please return {0:c}", result);
                        }
                        break;
                        
                    default:
                        if (!Kassa.handleBarcode(input)) {
                            Console.Out.WriteLine("Product could not be added");
                        }

                        break;
                }
                ShowMenu();
                input = Console.ReadLine().ToLower().Trim();
            }
        }

        private void ShowMenu() {
            Console.Out.WriteLine("P         : Toon alle producten");
            Console.Out.WriteLine("<Barcode> : Scan de barcode code");
            Console.Out.WriteLine("B<bedrag> : Doe een betaling");
            Console.Out.WriteLine("Q         : Programma afbreken");
            Console.Out.Write("> ");
        }

        public void DisplayClientScreen(string line1, string line2) {
            Console.Out.WriteLine("==========================================");
            Console.Out.WriteLine("|{0,-40}|", line1);
            Console.Out.WriteLine("|{0,-40}|", line2);
            Console.Out.WriteLine("==========================================");
        }

        public void DisplayProducts(IList<IProduct> products) {
            Console.Out.WriteLine("======================== products =======================");
            foreach (IProduct product in products) {
                Console.Out.WriteLine(String.Format("{0,4} {1,-40} {2:c}",
                    product.Barcode, product.Description, product.Amount));
            }
            Console.Out.WriteLine("=========================================================");
        }
    }
}
