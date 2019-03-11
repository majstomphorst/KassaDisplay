using System;
using System.Collections;
using System.Collections.Generic;
using EventHandeling;
using NUnit.Framework;

namespace EventHandelingTest
{
    [TestFixture]
    public class TestKassa
    {
        public List<IProduct> ProductList;
        public ProductCatalogus ProductCatalogus;
        public IKassa Kassa;
        public ConsoleKassaInterface Console;

        public void PrepareNormalKassa()
        {
            ProductList = new List<IProduct>
            {
                new Product("01", "Test product 01!", 0.01m),
                new Product("02", "Test product 02!", 0.02m)
            };
            ProductCatalogus = new ProductCatalogus(ProductList);
            Kassa = new Kassa(ProductCatalogus);
            Console = new ConsoleKassaInterface(Kassa);
            Kassa.setDisplay(Console);
        }

        [Test]
        public void TestKassaConstructor()
        {
            Assert.Throws<NullReferenceException>(() => new Kassa(null));
        }

        [Test]
        public void TestInitatePayment()
        {
            // repare
            PrepareNormalKassa();
            //var productList = new List<IProduct>
            //{
            //    new Product("01", "Test product 01!", 0.01m),
            //    new Product("02", "Test product 02!", 0.02m)
            //};
            decimal payedAmount = 1m;

            //var productCatalogus = new ProductCatalogus(productList);
            //IKassa kassa = new Kassa(productCatalogus);
            //ConsoleKassaInterface console = new ConsoleKassaInterface(kassa);
            //kassa.setDisplay(console);

            // test
            decimal i = 0m;

            Kassa.handleBarcode("01");
            Kassa.handleBarcode("02");

           i = Kassa.initatePayment(payedAmount);
           
            // validate
            if (i != 0.97m)
            {
                Assert.Fail("sum is incorrect! {0}",i);
            }
        }



        [Test]
        public void TesthandleBarcode()
        {
            // prepare
            var productList = new List<IProduct>
            {
                new Product("01", "Test product 01!", 0.01m),
                new Product("02", "Test product 02!", 0.02m)
            };

            var productCatalogus = new ProductCatalogus(productList);
            IKassa kassa = new Kassa(productCatalogus);
            ConsoleKassaInterface console = new ConsoleKassaInterface(kassa);
            kassa.setDisplay(console);

            // test
            var test1 = kassa.handleBarcode("01");
            var test2 = kassa.handleBarcode("");

            // validate
            if (test1 != true)
            {
                Assert.Fail("Product not found where one should be!");
            }

            if (test2 != false)
            {
                Assert.Fail("Product found where none should be!");
            }




        }
    }
}
