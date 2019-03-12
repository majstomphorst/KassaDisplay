using EventHandeling;
using NUnit.Framework;


namespace EventHandelingTest
{
    [TestFixture]
    public class ProductCatalogusTest
    {

        [Test]
        public void TestFindProductForBarcode()
        {
            // repare
            var catalogus = new ProductCatalogus();

            // test
            var test04 = catalogus.FindProductForBarcode("04");
            var test01 = catalogus.FindProductForBarcode("01");
            var testKaas = catalogus.FindProductForBarcode("kaas");

            // validate
            Assert.IsNull(test04);

            Assert.IsNotNull(test01);
            Assert.AreEqual("01", test01.Barcode);

            Assert.IsNull(testKaas);
        }

        [Test]
        public void TestGetAllProducts()
        {
            // repare
            var catalogus = new ProductCatalogus();

            // test
            var allProducts = catalogus.GetAllProducts();

            // validate
            Assert.IsNotNull(allProducts);
            Assert.AreEqual(03, allProducts.Count);
            Assert.AreEqual("Buy this product: It is sick!", allProducts[0].Description);

        }



    }
}
