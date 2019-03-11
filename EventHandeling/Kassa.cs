using System;
using System.Collections.Generic;
using System.Linq;

namespace EventHandeling
{
    public class Kassa : IKassa
    {
        public IProductCatalogus Catalogus { get; set; }
        private IKassaDisplay Display { get; set; } = null;
        public List<IProduct> Cart { get; private set; } = new List<IProduct>();

        public event EventHandler<BarcodeScandedEventArgs> BarcodeScanded;
        public event EventHandler<PaymentMadeEventArgs> PaymentMade;

        private static EventArgs GetEmpty()
        {
            return EventArgs.Empty;
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
                RaiseBarcodeScaned(barcode);
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
                RaisePayment();
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

        protected virtual void RaiseBarcodeScaned(string barcode)
        {
            // check if there are any subscribers to this event
            if (BarcodeScanded != null)
            {
                BarcodeScanded(this, new BarcodeScandedEventArgs()
                {
                    Barcode = barcode
                });
            }
        }
        protected virtual void RaisePayment()
        {
            if (PaymentMade != null)
            {
                PaymentMade(this, new PaymentMadeEventArgs()
                {
                    Cart = this.Cart
                });
            }
        }

    }
}