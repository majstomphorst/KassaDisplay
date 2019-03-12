using System;
using System.Collections;
using System.Collections.Generic;
using EventHandeling;
using NUnit.Framework;
using static EventHandelingTest.ProductCatalogus;

namespace EventHandelingTest
{
    [TestFixture]
    public class TestKassa
    {

        [Test]
        public void TestKassaConstructor()
        {
            Assert.Throws<NullReferenceException>(() => new Kassa(null));
        }


        [Test]
        public void TestInitatePayment()
        {

            // prepare
            var cat = new TestProductCatalogus();
            cat.ReturnProduct = new Product("01", "Test product 01!", 0.01m);
            var kassa = new Kassa(cat);

            kassa.handleBarcode("");
            kassa.handleBarcode("");


            // test
            decimal payedAmount = 1m;

            decimal i = kassa.initatePayment(payedAmount);

            // validate
            Assert.AreEqual(0.98m, i);
        }

        [Test]
        public void TestInitatePaymentEmptyCart()
        {
            // prepare
            var cat = new TestProductCatalogus();
            var kassa = new Kassa(cat);

            // test
            decimal payedAmount = 1m;

            decimal i = kassa.initatePayment(payedAmount);

            // validate
            Assert.AreEqual(1m, i);
        }

        [Test]
        public void TestInitatePaymentNotEnoughMoney()
        {

            // prepare
            var cat = new TestProductCatalogus();
            cat.ReturnProduct = new Product("01", "Test product 01!", 0.80m);
            var kassa = new Kassa(cat);

            kassa.handleBarcode("");
            kassa.handleBarcode("");


            // test
            decimal payedAmount = 1m;

            decimal i = kassa.initatePayment(payedAmount);

            // validate
            Assert.AreEqual(-1m, i);
        }

        [Test]
        public void TestInitatePaymentExactlyEnoughMoney()
        {
            // prepare
            var cat = new TestProductCatalogus();
            cat.ReturnProduct = new Product("01", "Test product 01!", 0.50m);
            var kassa = new Kassa(cat);

            kassa.handleBarcode("");
            kassa.handleBarcode("");

            // test
            decimal payedAmount = 1m;

            decimal i = kassa.initatePayment(payedAmount);

            // validate
            Assert.AreEqual(0m, i);
        }

         [Test]
         public void TestHandleBarcode()
         {
            // prepare
            var barcodes = new List<BarcodeEventArgs>();
            var cat = new TestProductCatalogus();
            var kassa = new Kassa(cat);

            kassa.BarcodeScanned += (sender, e) =>
            {
                barcodes.Add(e);
            };

            // test
            cat.ReturnProduct = new Product("01", "Test product 01!", 0.50m);
            var test1 = kassa.handleBarcode("01");

            cat.ReturnProduct = null;
            var test2 = kassa.handleBarcode("");

            cat.ReturnProduct = new Product("03", "Test product 03!", 0.50m);
            var test3 = kassa.handleBarcode("04");

            // validate
            Assert.IsTrue(test1, "Product 01 not found where one should be!");
            Assert.IsFalse(test2, "Product 02 found where none should be!");
            Assert.IsTrue(test3, "Product 04 not found where one should be!");

            Assert.AreEqual(2, barcodes.Count);
            Assert.AreEqual("01", barcodes[0].Barcode);
            Assert.AreEqual("04", barcodes[1].Barcode);
         }

        [Test]
        public void TestHandleBarcodeClientDisplay()
        {
            // prepare
            var displays = new List<(decimal total,IProduct product)>();
            var cat = new TestProductCatalogus();
            var kassa = new Kassa(cat);

            kassa.DisplayToClient += (sender, e) =>
            {
                displays.Add((e.TotalPrice,e.Product));
            };

            // test
            cat.ReturnProduct = new Product("01", "Test product 01!", 0.50m);
            var test1 = kassa.handleBarcode("01");

            cat.ReturnProduct = null;
            var test2 = kassa.handleBarcode("");

            cat.ReturnProduct = new Product("03", "Test product 03!", 0.75m);
            var test3 = kassa.handleBarcode("04");

            // validate
            Assert.AreEqual(2, displays.Count);

            Assert.AreEqual(0.50m, displays[0].total);
            Assert.IsNotNull(displays[0].product);
            Assert.AreEqual("Test product 01!", displays[0].product.Description);

            Assert.AreEqual(1.25m, displays[1].total);
            Assert.IsNotNull(displays[1].product);
            Assert.AreEqual("Test product 03!", displays[1].product.Description);
        }

        [Test]
        public void TestShowAllProducts()
        {
            // prepare
            IList<IProduct> displays = null;
            var cat = new TestProductCatalogus();
            cat.ReturnProductList = new List<IProduct>
                {
                    new Product("01", "Test product 01!", 0.01m),
                    new Product("02", "Test product 02!", 0.02m)
                };
            var kassa = new Kassa(cat);

            kassa.DisplayAllProducts += (sender, e) =>
            {
                displays = e.Products;
            };

            // test
            kassa.showAllProducts();

            // validate
            Assert.IsNotNull(displays);
            Assert.AreEqual(2, displays.Count);

            Assert.AreEqual(cat.ReturnProductList, displays);


        }

        [Test]
        public void TestShowNoProducts()
        {
            // prepare
            IList<IProduct> displays = null;
            var cat = new TestProductCatalogus();
          
            var kassa = new Kassa(cat);

            kassa.DisplayAllProducts += (sender, e) =>
            {
                displays = e.Products;
            };

            // test
            kassa.showAllProducts();

            // validate
            Assert.IsNotNull(displays);
            Assert.AreEqual(0, displays.Count);


        }

    }
    }
