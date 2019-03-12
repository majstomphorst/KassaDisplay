using System;
using System.Collections.Generic;
using System.Linq;

namespace EventHandeling
{
    public class Kassa
    {
        public IProductCatalogus Catalogus { get; set; }
        // private IKassaDisplay Display { get; set; } = null;
        public List<IProduct> Cart { get; private set; } = new List<IProduct>();
        public event EventHandler<BarcodeEventArgs> BarcodeScanned;
        public event EventHandler<PaymentMadeEventArgs> PaymentMade;
        public event EventHandler<DisplayEventArgs> DisplayToClient;
        public event EventHandler<ProductsEventArgs> DisplayAllProducts;

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
                RaiseBarcodeScanned(barcode);
                RaiseClientDisplay(GetTotalCartPrice(), product);
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

        /// <summary>
        /// Show all products
        /// </summary>
        public void showAllProducts()
        {
            var allProducts = Catalogus.GetAllProducts();
            RaiseDisplayAllProducts(allProducts);

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

        protected virtual void RaiseDisplayAllProducts(IList<IProduct> list)
        {
            if (DisplayAllProducts != null)
            {
                DisplayAllProducts(this, new ProductsEventArgs{
                    Products = list
                });
            }
        }

        protected virtual void RaiseClientDisplay(decimal totalCartPrice, IProduct product )
        {
            if (DisplayToClient != null)
            {
                DisplayToClient(this, new DisplayEventArgs(totalCartPrice, product));
            }
        }

        protected virtual void RaiseBarcodeScanned(string barcode)
        {
            // check if there are any subscribers to this event
            if (BarcodeScanned != null)
            {
                BarcodeScanned(this, new BarcodeEventArgs(barcode));
            }
        }
        protected virtual void RaisePayment()
        {
            if (PaymentMade != null)
            {
                PaymentMade(this, new PaymentMadeEventArgs(Cart));
            }
        }

    }
}