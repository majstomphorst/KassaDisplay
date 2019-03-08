using System;
using System.Collections.Generic;
using System.Linq;

namespace EventHandeling
{
    internal class Kassa : IKassa
    {
        public IProductCatalogus Catalogus { get; set; }
        private IKassaDisplay Display { get; set; } = null;
        public List<IProduct> Cart { get; set; } = new List<IProduct>();

        public Kassa(IProductCatalogus catalogus)
        {
            Catalogus = catalogus;
        }

        /// <summary>
        /// Handle a scan of a barcode
        /// </summary>
        /// <param name="barcode">The scanned barcode</param>
        /// <returns><c>true</c> if succesfull, <c>false</c> otherwise</returns>
        bool IKassa.handleBarcode(string barcode)
        {
            if (Display != null)
            {
                var product = Catalogus.FindProductForBarcode(barcode);
                if (product != null)
                {
                    Cart.Add(product);
                    Display.DisplayClientScreen(String.Format("TOTAAL {0:c}", GetTotalCartPrice()), product.ToString());
                    Display.DisplayProducts(Cart);
                    return true;
                }
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