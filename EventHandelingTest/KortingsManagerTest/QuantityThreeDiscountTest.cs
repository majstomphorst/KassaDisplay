using System;
using System.Collections;
using System.Collections.Generic;
using EventHandeling;
using NUnit.Framework;

namespace EventHandelingTest
{
    [TestFixture]
    public class QuantityThreeDiscountTest
    {
        [Test]
        public void TestSingleDiscount() 
        {
            // prepare
            var discountProducts = new List<IProduct>();
            var km = new KortingsManager();
            km.QuantityThreeDiscount();

            var product1 = new BarcodeEventArgs(new Product("01", "Test product 01!", 1m));
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
            Assert.AreEqual("DISCOUNT ON" + product1.Product.Description , discountProducts[0].Description);
            Assert.AreEqual(-0.75m,discountProducts[0].Amount);
        }

        [Test]
        public void TestSingleDiscounWithCheckOutAfter3() 
        {
            // prepare
            var discountProducts = new List<IProduct>();
            var km = new KortingsManager();
            km.QuantityThreeDiscount();
            var product1 = new BarcodeEventArgs(new Product("01", "Test product 01!", 1m));

            km.DiscountAProduct += (sender, e) =>
            {
                discountProducts.Add(e.Product);
            };
            
            // test
            km.RaiseBarcodeScaned(null,product1);
            km.RaiseBarcodeScaned(null,product1);
            km.RaiseBarcodeScaned(null,product1);
            km.RaiseBarcodeScaned(null,product1);
            km.RaiseBarcodeScaned(null,product1);

            // validate
            Assert.IsNotEmpty(discountProducts);
            Assert.AreEqual(1, discountProducts.Count);
            Assert.AreEqual(-0.75m, discountProducts[0].Amount);
            Assert.AreEqual("DISCOUNT ON" + product1.Product.Description , discountProducts[0].Description);

            Assert.AreEqual(2,product1.Product.Amount + product1.Product.Amount);
        }

        [Test]
        public void TestNODiscounWithCheckOutAfter2() 
        {
            // prepare
            var discountProducts = new List<IProduct>();
            var km = new KortingsManager();
            km.QuantityThreeDiscount();
            var product1 = new BarcodeEventArgs(new Product("01", "Test product 01!", 1m));

            km.DiscountAProduct += (sender, e) =>
            {
                discountProducts.Add(e.Product);
            };
            
            // test
            km.RaiseBarcodeScaned(null,product1);
            km.RaiseBarcodeScaned(null,product1);
            km.RaisePayment(null,null);
            km.RaiseBarcodeScaned(null,product1);
            km.RaiseBarcodeScaned(null,product1);

            // validate
            Assert.IsEmpty(discountProducts);
        }

        [Test]
        public void TestDiscountTwiseSameProduct() 
        {
            // prepare
            var discountProducts = new List<IProduct>();
            var km = new KortingsManager();
            km.QuantityThreeDiscount();
            var product1 = new BarcodeEventArgs(new Product("01", "Test product 01!", 1m));

            km.DiscountAProduct += (sender, e) =>
            {
                discountProducts.Add(e.Product);
            };
            
            // test
            km.RaiseBarcodeScaned(null,product1);
            km.RaiseBarcodeScaned(null,product1);
            km.RaiseBarcodeScaned(null,product1);
            km.RaiseBarcodeScaned(null,product1);
            km.RaiseBarcodeScaned(null,product1);
            km.RaiseBarcodeScaned(null,product1);

            // validate
            Assert.IsNotEmpty(discountProducts);
            Assert.AreEqual(2, discountProducts.Count);
            Assert.AreEqual(-0.75m, discountProducts[0].Amount);
            Assert.AreEqual(-0.75m, discountProducts[1].Amount);

            Assert.AreEqual("DISCOUNT ON" + product1.Product.Description , discountProducts[0].Description);
            Assert.AreEqual("DISCOUNT ON" + product1.Product.Description , discountProducts[1].Description);

        }

        [Test]
        public void TestDiscountTwiseDifferentGetProducts() 
        {
            // prepare
            var discountProducts = new List<IProduct>();
            var km = new KortingsManager();
            km.QuantityThreeDiscount();
            var product1 = new BarcodeEventArgs(new Product("01", "Test product 01!", 1m));
            var product2 = new BarcodeEventArgs(new Product("02", "Test product 02!", 1.4m));

            km.DiscountAProduct += (sender, e) =>
            {
                discountProducts.Add(e.Product);
            };
            
            // test
            km.RaiseBarcodeScaned(null,product1);
            km.RaiseBarcodeScaned(null,product1);
            km.RaiseBarcodeScaned(null,product1);

            km.RaiseBarcodeScaned(null,product2);
            km.RaiseBarcodeScaned(null,product2);
            km.RaiseBarcodeScaned(null,product2);

            // validate
            Assert.IsNotEmpty(discountProducts);
            Assert.AreEqual(2, discountProducts.Count);
            Assert.AreEqual("01",discountProducts[0].Barcode);
            Assert.AreEqual("02",discountProducts[1].Barcode);


            Assert.AreEqual(-0.75m, discountProducts[0].Amount);
            Assert.AreEqual(-1.05m, discountProducts[1].Amount);

            Assert.AreEqual("DISCOUNT ON" + product1.Product.Description , discountProducts[0].Description);
            Assert.AreEqual("DISCOUNT ON" + product2.Product.Description , discountProducts[1].Description);

        }
    }
}