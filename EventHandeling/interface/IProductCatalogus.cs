using System.Collections.Generic;

namespace EventHandeling
{
    public interface IProductCatalogus
    {
        /// <summary>
        /// Find a product for a barcode
        /// </summary>
        /// <returns>the product or <c>null</c> if not found</returns>
        IProduct FindProductForBarcode(string barcode);

        /// <returns>a list of all products</returns>
        IList<IProduct> GetAllProducts();
    }
}