using System;
using System.Collections;
using System.Collections.Generic;
using EventHandeling;
using NUnit.Framework;

namespace EventHandelingTest
{
    [TestFixture]
    public class CombinationDiscountOfProductTest
    {
        [Test]
        public void TestSingleDiscount() 
        {
            // prepare
            var discountProducts = new List<IProduct>();
            var km = new KortingsManager();

            var barcodes = new List<string>();
            barcodes.Add("02");
            barcodes.Add("01");

            km.CombinationDiscountOfProduct(barcodes,5);

            var product1 = new BarcodeEventArgs(new Product("01", "Test product 01!", 1.76m));
            var product2 = new BarcodeEventArgs(new Product("02", "Test product 02!", 4m));


            km.DiscountAProduct += (sender, e) =>
            {
                discountProducts.Add(e.Product);
            };
            
            // test
            km.RaiseBarcodeScaned(null,product1);
            km.RaiseBarcodeScaned(null,product2);
            km.RaiseBarcodeScaned(null,product1);
            km.RaiseBarcodeScaned(null,product2);
            km.RaiseBarcodeScaned(null,product1);

            // validate
            Assert.IsNotEmpty(discountProducts);
            Assert.AreEqual(1, discountProducts.Count);
            Assert.AreEqual("discount-cheapest-01-02-01-02-01-", discountProducts[0].Description);
            Assert.AreEqual(-1.76m,discountProducts[0].Amount);
        }

        [Test]
        public void TestSingleDiscounWithCheckOutAfter6() 
        {
            // prepare
            var discountProducts = new List<IProduct>();
            var km = new KortingsManager();

            var barcodes = new List<string>();
            barcodes.Add("02");
            barcodes.Add("01");

            km.CombinationDiscountOfProduct(barcodes,5);

            var product1 = new BarcodeEventArgs(new Product("01", "Test product 01!", 1.76m));
            var product2 = new BarcodeEventArgs(new Product("02", "Test product 02!", 4m));
            var product3 = new BarcodeEventArgs(new Product("03", "Test product 02!", 800m));

            km.DiscountAProduct += (sender, e) =>
            {
                discountProducts.Add(e.Product);
            };
            
            // test
            km.RaiseBarcodeScaned(null,product1);
            km.RaiseBarcodeScaned(null,product2);
            km.RaiseBarcodeScaned(null,product1);
            km.RaiseBarcodeScaned(null,product2);
            km.RaiseBarcodeScaned(null,product1);
            km.RaiseBarcodeScaned(null,product3);


            // validate
            Assert.IsNotEmpty(discountProducts);
            Assert.AreEqual(1, discountProducts.Count);
            Assert.AreEqual(-1.76m, discountProducts[0].Amount);
            Assert.AreEqual("discount-cheapest-01-02-01-02-01-", discountProducts[0].Description);
        }

        [Test]
        public void TestNoDiscounWithCheckOutAfter4() 
        {
            // prepare
            var discountProducts = new List<IProduct>();
            var km = new KortingsManager();

            var barcodes = new List<string>();
            barcodes.Add("02");
            barcodes.Add("01");

            km.CombinationDiscountOfProduct(barcodes,5);

            var product1 = new BarcodeEventArgs(new Product("01", "Test product 01!", 1.76m));
            var product2 = new BarcodeEventArgs(new Product("02", "Test product 02!", 4m));

            km.DiscountAProduct += (sender, e) =>
            {
                discountProducts.Add(e.Product);
            };
            
            // test
            km.RaiseBarcodeScaned(null,product1);
            km.RaiseBarcodeScaned(null,product2);
            km.RaiseBarcodeScaned(null,product1);
            km.RaiseBarcodeScaned(null,product2);

            // validate
            Assert.IsEmpty(discountProducts);
        }
    }
}