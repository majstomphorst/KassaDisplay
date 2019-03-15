using System;
using System.Collections;
using System.Collections.Generic;
using EventHandeling;
using NUnit.Framework;

namespace EventHandelingTest
{
    [TestFixture]
    public class QuantityCombinationDiscountTest
    {
        [Test]
        public void TestQuantityCombinationDiscountSingleProduct()
        {
            // test
            var discountProducts = new List<IProduct>();
            var km = new KortingsManager();
            var barcodes = new List<string>();
            barcodes.Add("02");
            barcodes.Add("01");
            km.addQuantityCombinationDiscount(barcodes, 5, 0.10m);
            var product1 = new BarcodeEventArgs(new Product("02", "Test product 02!", 1m));

            km.DiscountAProduct += (sender, e) =>
            {
                discountProducts.Add(e.Product);
            };

            // test
            km.RaiseBarcodeScaned(null, product1);
            km.RaiseBarcodeScaned(null, product1);
            km.RaiseBarcodeScaned(null, product1);
            km.RaiseBarcodeScaned(null, product1);
            km.RaiseBarcodeScaned(null, product1);

            // validate
            Assert.IsNotEmpty(discountProducts);
            Assert.AreEqual(1, discountProducts.Count);
            Assert.AreEqual("D-", discountProducts[0].Barcode);
            Assert.AreEqual(-0.50m, discountProducts[0].Amount);
        }

        [Test]
        public void TestQuantityCombinationDiscountWithThreeProducts()
        {
            // test
            var discountProducts = new List<IProduct>();
            var km = new KortingsManager();
            var barcodes = new List<string>();
            barcodes.Add("02");
            barcodes.Add("03");

            km.addQuantityCombinationDiscount(barcodes, 5 ,0.2m);

            var product1 = new BarcodeEventArgs(new Product("01", "Test product 01!", 1m));
            var product2 = new BarcodeEventArgs(new Product("02", "Test product 02!", 1.4m));
            var product3 = new BarcodeEventArgs(new Product("03", "Test product 03!", 11m));

            km.DiscountAProduct += (sender, e) =>
            {
                discountProducts.Add(e.Product);
            };

            // test
            km.RaiseBarcodeScaned(null, product3);
            km.RaiseBarcodeScaned(null, product1);
            km.RaiseBarcodeScaned(null, product1);
            km.RaiseBarcodeScaned(null, product3);
            km.RaiseBarcodeScaned(null, product2);
            km.RaiseBarcodeScaned(null, product2);
            km.RaiseBarcodeScaned(null, product3); 

            // validate
            Assert.IsNotEmpty(discountProducts);
            Assert.AreEqual(1, discountProducts.Count);
            Assert.AreEqual("D-", discountProducts[0].Barcode);
            Assert.AreEqual(-7.16m, discountProducts[0].Amount);
        }
        [Test]
        public void TestQuantityCombinationNoDiscount() 
        {
            // test
            var discountProducts = new List<IProduct>();
            var km = new KortingsManager();
            var barcodes = new List<string>();
            barcodes.Add("02");
            barcodes.Add("03");

            km.addQuantityCombinationDiscount(barcodes, 4 ,0.2m);

            var product2 = new BarcodeEventArgs(new Product("02", "Test product 02!", 1.4m));
            var product3 = new BarcodeEventArgs(new Product("03", "Test product 03!", 11m));

            km.DiscountAProduct += (sender, e) =>
            {
                discountProducts.Add(e.Product);
            };

            // test
            km.RaiseBarcodeScaned(null, product3);
            km.RaiseBarcodeScaned(null, product3);
            km.RaiseBarcodeScaned(null, product2);

            // validate
            Assert.IsEmpty(discountProducts);
        }
    }
}