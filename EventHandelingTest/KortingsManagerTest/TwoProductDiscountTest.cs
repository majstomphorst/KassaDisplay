using System;
using System.Collections;
using System.Collections.Generic;
using EventHandeling;
using NUnit.Framework;

namespace EventHandelingTest
{
    [TestFixture]
    public class TwoProductDiscountTest
    {
        [Test]
        public void TestSingleDiscount() 
        {
            // prepare
            var discountProducts = new List<IProduct>();
            var km = new KortingsManager();

            var barcodes = new List<string>();
            barcodes.Add("01");
            barcodes.Add("02");

            km.TwoProductDiscount(barcodes, 2.99m);

            var product1 = new BarcodeEventArgs(new Product("01", "Test product 01!", 1.0m));
            var product2 = new BarcodeEventArgs(new Product("02", "Test product 02!", 4.0m));


            km.DiscountAProduct += (sender, e) =>
            {
                discountProducts.Add(e.Product);
            };
            
            // test
            km.RaiseBarcodeScaned(null,product1);
            km.RaiseBarcodeScaned(null,product2);

            // validate
            Assert.IsNotEmpty(discountProducts);
            Assert.AreEqual(1, discountProducts.Count);
            Assert.AreEqual("Discount-01-02", discountProducts[0].Description);
            Assert.AreEqual(-2.01m,discountProducts[0].Amount);
        }

        [Test]
        public void TestNoDiscount() 
        {
            // prepare
            var discountProducts = new List<IProduct>();
            var km = new KortingsManager();

            var barcodes = new List<string>();
            barcodes.Add("02");
            barcodes.Add("03");

            km.TwoProductDiscount(barcodes, 2.99m);

            var product1 = new BarcodeEventArgs(new Product("01", "Test product 01!", 1.0m));
            var product2 = new BarcodeEventArgs(new Product("02", "Test product 02!", 4.0m));


            km.DiscountAProduct += (sender, e) =>
            {
                discountProducts.Add(e.Product);
            };
            
            // test
            km.RaiseBarcodeScaned(null,product1);
            km.RaiseBarcodeScaned(null,product2);

            // validate
            Assert.IsEmpty(discountProducts);
        }

    }
}