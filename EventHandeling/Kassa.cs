using System;
using System.Collections.Generic;
using System.Linq;

namespace EventHandeling
{
    public class BarcodeScandedEventArgs : EventArgs 
    {
        public string Barcode { get; set; }
    }
    public class Kassa : IKassa
    {
        public IProductCatalogus Catalogus { get; set; }
        private IKassaDisplay Display { get; set; } = null;
        public List<IProduct> Cart { get; private set; } = new List<IProduct>();

        // 1. define a delagete
        public delegate void BarcodeScanedEventHandler(object source, BarcodeScandedEventArgs args);
         // 2, define event
        public event BarcodeScanedEventHandler BarcodeScanded;
        
        // 3. raise the event
        protected virtual void OnBarcodeScaned(string barcode)
        {
            // check if there are any subscribers to this event
            if (BarcodeScanded != null)
            {
                BarcodeScanded(this, new BarcodeScandedEventArgs(){
                    Barcode = barcode
                });
            }
        }
        
        
        public Kassa(IProductCatalogus catalogus)
        {
            Catalogus = catalogus ?? throw new NullReferenceException("catalogus");
        }

        /// <summary>
        /// Handle a scan of a barcode
        /// </summary>
        /// <param name="barcode">The scanned barcode</param>
        /// <returns><c>true</c> if succesfull, <c>false</c> otherwise</returns>
        public bool handleBarcode(string barcode)
        {
            var product = Catalogus.FindProductForBarcode(barcode);
            if (product != null)
            {
                Cart.Add(product);
                Display.DisplayClientScreen(String.Format("TOTAAL {0:c}", GetTotalCartPrice()), product.ToString());
                OnBarcodeScaned(barcode);
                return true;
            }
            
            return false;
        }

        /// <summary>
        /// Initiate a payment
        /// </summary>
        /// <param name="amount"></param>
        /// <returns>the amount to return or -1 on failure</returns>
        public decimal initatePayment(decimal amount)
        {
            if (Cart.Any())
            {
                amount = amount - GetTotalCartPrice();
                Cart = new List<IProduct>();
                return amount;
            }
            return -1;
        }

        public void setDisplay(IKassaDisplay kassaDisplay)
        {
            Display = kassaDisplay;
        }

        /// <summary>
        /// Show all products
        /// </summary>
        public void showAllProducts()
        {
            if (Display != null)
            {
                var i = Catalogus.GetAllProducts();
                Display.DisplayProducts(i);
            }
        }

        private decimal GetTotalCartPrice()
        {
            decimal count = 0m;
            if (Cart.Any())
            {
                foreach (var product in Cart)
                {
                    count += product.Amount;
                }
            }
            return count;
        }

    }
}