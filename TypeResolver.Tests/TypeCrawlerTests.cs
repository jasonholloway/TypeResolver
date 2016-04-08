using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace TypeResolver.Tests
{
    [TestClass]
    public class TypeCrawlerTests
    {

        [TestMethod]
        public void CrawlerReturnsNullIfBadMatch() 
        {
            var crawler = TypeCrawlerCreator.Visit(typeof(int));

            var result = crawler(typeof(long));

            Assert.IsNull(result);
        }

        
        [TestMethod]
        public void CrawlerReturnsEmptyEnumerationIfGoodSimpleMatch() 
        {
            var crawler = TypeCrawlerCreator.Visit(typeof(int));

            var result = crawler(typeof(int));

            Assert.IsNotNull(result);
            Assert.IsTrue(result.SequenceEqual(Enumerable.Empty<Type>()));
        }


        [TestMethod]
        public void CrawlerReturnsInPlaceGenArgument() 
        {
            var crawler = TypeCrawlerCreator.Visit(typeof(List<>));

            var result = crawler(typeof(List<int>));

            Assert.IsNotNull(result);
            Assert.IsTrue(result.SequenceEqual(new[] { typeof(int) }));
        }


        [TestMethod]
        public void CrawlerReturnsNestedInPlaceGenArgument() 
        {
            var crawler = TypeCrawlerCreator.Visit(typeof(List<>).MakeGenericType(typeof(List<>)));

            var result = crawler(typeof(List<List<int>>));

            Assert.IsNotNull(result);
            Assert.IsTrue(result.SequenceEqual(new[] { typeof(int) }));
        }







    }
}
