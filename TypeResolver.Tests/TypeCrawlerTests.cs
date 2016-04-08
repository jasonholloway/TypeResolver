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
            throw new NotImplementedException();
        }








    }
}
