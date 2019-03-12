//using System;
//using System.Collections;
//using System.Collections.Generic;
//using EventHandeling;
//using NUnit.Framework;


//namespace EventHandelingTest
//{
//    [TestFixture]
//    public class TestProductCatalogus
//    {
//        public List<IProduct> ProductList;
//        public ProductCatalogus ProductCatalogus;
//        public IKassa Kassa;
//        public ConsoleKassaInterface Console;

//        public void PrepareNormalKassa()
//        {
//            ProductList = new List<IProduct>
//            {
//                new Product("01", "Test product 01!", 0.01m),
//                new Product("02", "Test product 02!", 0.02m)
//            };
//            // ProductCatalogus = new ProductCatalogus(ProductList);
//            // Kassa.setDisplay(Console);
//        }

//        //[Test]
//        //public void TestFindProductForBarcode()
//        //{
//        //    // repare
//        //    PrepareNormalKassa();

//        //    // test
//        //    var isNull = ProductCatalogus.FindProductForBarcode("04");
//        //    var product = ProductCatalogus.FindProductForBarcode("01");
//        //    var product2 = ProductCatalogus.FindProductForBarcode("kaas");

//        //    // validate
//        //    if (isNull != null && product2 != null)
//        //    {
//        //        Assert.Fail("Product found where none should be.");
//        //    }

//        //    if (product == null)
//        //    {
//        //        Assert.Fail("No product found.");
//        //    }
//        //}

//    }
//}
