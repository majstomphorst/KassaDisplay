namespace EventHandeling
{
    public interface IKassa
    {
        void setDisplay(IKassaDisplay kassaDisplay);

        /// <summary>
        /// Handle a scan of a barcode
        /// </summary>
        /// <param name="barcode">The scanned barcode</param>
        /// <returns><c>true</c> if succesfull, <c>false</c> otherwise</returns>
        bool handleBarcode(string barcode);

        /// <summary>
        /// Initiate a payment
        /// </summary>
        /// <param name="amount"></param>
        /// <returns>the amount to return or -1 on failure</returns>
        decimal initatePayment(decimal amount);

        /// <summary>
        /// Show all products
        /// </summary>
        void showAllProducts();
    }
}